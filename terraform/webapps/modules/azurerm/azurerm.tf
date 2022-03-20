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
  name     = "${var.prefix}-aspnet-nodejs-re01"
  location = var.location
  tags = {
    "Environment" = var.environment
    "Developer"   = "Romayne Eastmond"
  }
}

resource "azurerm_mssql_server" "main_mssql_server" {
  name                         = "${var.prefix}-${var.mssql_server}"
  resource_group_name          = azurerm_resource_group.main_resource_group.name
  location                     = azurerm_resource_group.main_resource_group.location
  version                      = "12.0"
  administrator_login          = "sysadmin-company02"
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

resource "azurerm_app_service_plan" "main_app_service_plan_01" {
  name                = "ASP-${var.prefix}-aspnetnodejsre01-bg17"
  resource_group_name = azurerm_resource_group.main_resource_group.name
  location            = azurerm_resource_group.main_resource_group.location

  sku {
    tier = "Free"
    size = "F1"
  }
}

resource "azurerm_app_service_plan" "main_app_service_plan_02" {
  name                = "ASP-${var.prefix}-aspnetnodejsre01-bg92"
  resource_group_name = azurerm_resource_group.main_resource_group.name
  location            = azurerm_resource_group.main_resource_group.location
  kind                = "Linux"
  reserved            = true

  sku {
    tier = "Free"
    size = "F1"
  }
}

resource "azurerm_app_service" "main_company_course" {
  name                = "${var.prefix}-aspnet-company-course-re01"
  resource_group_name = azurerm_resource_group.main_resource_group.name
  location            = azurerm_resource_group.main_resource_group.location
  app_service_plan_id = azurerm_app_service_plan.main_app_service_plan_01.id

  site_config {
    dotnet_framework_version  = "v6.0"
    use_32_bit_worker_process = true
  }

  app_settings = {
    "RabbitMQAvailable" = false
  }

  connection_string {
    name  = "MicroserviceDbString"
    type  = "SQLAzure"
    value = "Data Source=tcp:${azurerm_mssql_server.main_mssql_server.name}.database.windows.net,1433;Initial Catalog=CompanyMicroservicesCourses;User Id=${azurerm_mssql_server.main_mssql_server.administrator_login}@${azurerm_mssql_server.main_mssql_server.name};Password=${azurerm_mssql_server.main_mssql_server.administrator_login_password}"
  }

  tags = {
    "Environment" = var.environment
    "Developer"   = "Romayne Eastmond"
    "Language"    = "ASP.NET Framework"
  }
}

resource "azurerm_app_service" "main_company_department" {
  name                = "${var.prefix}-aspnet-company-department-re01"
  resource_group_name = azurerm_resource_group.main_resource_group.name
  location            = azurerm_resource_group.main_resource_group.location
  app_service_plan_id = azurerm_app_service_plan.main_app_service_plan_01.id

  site_config {
    dotnet_framework_version  = "v6.0"
    use_32_bit_worker_process = true
  }

  app_settings = {
    "RabbitMQAvailable" = false
  }

  connection_string {
    name  = "MicroserviceDbString"
    type  = "SQLAzure"
    value = "Data Source=tcp:${azurerm_mssql_server.main_mssql_server.name}.database.windows.net,1433;Initial Catalog=CompanyMicroservicesDepartments;User Id=${azurerm_mssql_server.main_mssql_server.administrator_login}@${azurerm_mssql_server.main_mssql_server.name};Password=${azurerm_mssql_server.main_mssql_server.administrator_login_password}"
  }

  tags = {
    "Environment" = var.environment
    "Developer"   = "Romayne Eastmond"
    "Language"    = "ASP.NET Framework"
  }
}

resource "azurerm_app_service" "main_company_employee" {
  name                = "${var.prefix}-aspnet-company-employee-re01"
  resource_group_name = azurerm_resource_group.main_resource_group.name
  location            = azurerm_resource_group.main_resource_group.location
  app_service_plan_id = azurerm_app_service_plan.main_app_service_plan_01.id

  site_config {
    dotnet_framework_version  = "v6.0"
    use_32_bit_worker_process = true
  }

  app_settings = {
    "RabbitMQAvailable" = false
  }

  connection_string {
    name  = "MicroserviceDbString"
    type  = "SQLAzure"
    value = "Data Source=tcp:${azurerm_mssql_server.main_mssql_server.name}.database.windows.net,1433;Initial Catalog=CompanyMicroservicesEmployees;User Id=${azurerm_mssql_server.main_mssql_server.administrator_login}@${azurerm_mssql_server.main_mssql_server.name};Password=${azurerm_mssql_server.main_mssql_server.administrator_login_password}"
  }

  tags = {
    "Environment" = var.environment
    "Developer"   = "Romayne Eastmond"
    "Language"    = "ASP.NET Framework"
  }
}

resource "azurerm_app_service" "main_company_notification" {
  name                = "${var.prefix}-aspnet-company-notification-re01"
  resource_group_name = azurerm_resource_group.main_resource_group.name
  location            = azurerm_resource_group.main_resource_group.location
  app_service_plan_id = azurerm_app_service_plan.main_app_service_plan_01.id

  site_config {
    dotnet_framework_version  = "v6.0"
    use_32_bit_worker_process = true
  }

  app_settings = {
    "RabbitMQAvailable" = false
  }

  connection_string {
    name  = "MicroserviceDbString"
    type  = "SQLAzure"
    value = "Data Source=tcp:${azurerm_mssql_server.main_mssql_server.name}.database.windows.net,1433;Initial Catalog=CompanyMicroservicesNotifications;User Id=${azurerm_mssql_server.main_mssql_server.administrator_login}@${azurerm_mssql_server.main_mssql_server.name};Password=${azurerm_mssql_server.main_mssql_server.administrator_login_password}"
  }

  tags = {
    "Environment" = var.environment
    "Developer"   = "Romayne Eastmond"
    "Language"    = "ASP.NET Framework"
  }
}

resource "azurerm_app_service" "main_microservices_catalogue" {
  name                = "${var.prefix}-nodejs-microservices-catalogue-re01"
  resource_group_name = azurerm_resource_group.main_resource_group.name
  location            = azurerm_resource_group.main_resource_group.location
  app_service_plan_id = azurerm_app_service_plan.main_app_service_plan_02.id

  site_config {
    linux_fx_version = "NODE|14-lts"
    app_command_line = "pm2 serve /home/site/wwwroot --no-daemon --spa"
  }

  tags = {
    "Environment" = var.environment
    "Developer"   = "Romayne Eastmond"
    "Language"    = "Node.js"
    "SPA"         = "React"
  }
}
