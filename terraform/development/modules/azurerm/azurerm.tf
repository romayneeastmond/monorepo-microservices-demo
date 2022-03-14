terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
    }
  }
}

resource "random_password" "password" {
  length           = 16
  special          = true
  override_special = "_%@"
}

resource "azurerm_resource_group" "main_resource_group" {
  name     = "${var.prefix}-kubernetes-cluster-re02-rg"
  location = var.location
  tags = {
    "Environment" = var.environment
  }
}

resource "azurerm_mssql_server" "main_mssql_server" {
  name                         = "${var.prefix}-${var.mssql_server}"
  resource_group_name          = azurerm_resource_group.main_resource_group.name
  location                     = azurerm_resource_group.main_resource_group.location
  version                      = "12.0"
  administrator_login          = "sysadmin-company01"
  administrator_login_password = random_password.password.result
  minimum_tls_version          = "1.2"

  tags = {
    "Environment" = var.environment
  }
}

resource "azurerm_sql_firewall_rule" "main_mssql_server_firewall_rule" {
  name                = "${var.prefix}-${var.mssql_server}-firewall-rule-01"
  resource_group_name = azurerm_resource_group.main_resource_group.name
  server_name         = azurerm_mssql_server.main_mssql_server.name
  start_ip_address    = "0.0.0.0"
  end_ip_address      = "0.0.0.0"
}

resource "azurerm_mssql_database" "main_mssql_database_microservices_courses" {
  name         = "CompanyMicroservicesCourses"
  server_id    = azurerm_mssql_server.main_mssql_server.id
  collation    = "SQL_Latin1_General_CP1_CI_AS"
  license_type = "LicenseIncluded"
  max_size_gb  = 2
  sku_name     = "Basic"

  tags = {
    "Environment" = var.environment
  }
}

resource "azurerm_mssql_database" "main_mssql_database_microservices_departments" {
  name         = "CompanyMicroservicesDepartments"
  server_id    = azurerm_mssql_server.main_mssql_server.id
  collation    = "SQL_Latin1_General_CP1_CI_AS"
  license_type = "LicenseIncluded"
  max_size_gb  = 2
  sku_name     = "Basic"

  tags = {
    "Environment" = var.environment
  }
}

resource "azurerm_mssql_database" "main_mssql_database_microservices_employees" {
  name         = "CompanyMicroservicesEmployees"
  server_id    = azurerm_mssql_server.main_mssql_server.id
  collation    = "SQL_Latin1_General_CP1_CI_AS"
  license_type = "LicenseIncluded"
  max_size_gb  = 2
  sku_name     = "Basic"

  tags = {
    "Environment" = var.environment
  }
}

resource "azurerm_mssql_database" "main_mssql_database_microservices_notifications" {
  name         = "CompanyMicroservicesNotifications"
  server_id    = azurerm_mssql_server.main_mssql_server.id
  collation    = "SQL_Latin1_General_CP1_CI_AS"
  license_type = "LicenseIncluded"
  max_size_gb  = 2
  sku_name     = "Basic"

  tags = {
    "Environment" = var.environment
  }
}

resource "azurerm_kubernetes_cluster" "main_kubernetes_cluster" {
  name                = "${var.prefix}-kubernetes-cluster-re02"
  resource_group_name = azurerm_resource_group.main_resource_group.name
  location            = azurerm_resource_group.main_resource_group.location
  tags = {
    "Environment" = var.environment
  }
  dns_prefix = "${var.prefix}kubernetesclusterre02"

  linux_profile {
    admin_username = "k8sclusteradmin"

    ssh_key {
      key_data = file(var.ssh_public_key)
    }
  }

  default_node_pool {
    name       = "default"
    node_count = 2
    vm_size    = "Standard_D2_v2"
  }

  service_principal {
    client_id     = var.service_principal
    client_secret = var.service_principal_secret
  }
}
