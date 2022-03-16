provider "azurerm" {
  subscription_id = var.subscription_id
  client_id       = var.service_principal
  client_secret   = var.service_principal_secret
  tenant_id       = var.tenant_id

  features {

  }
}

provider "kubernetes" {
  features {

  }
}

module "azurerm" {
  source                   = "./modules/azurerm/"
  service_principal        = var.service_principal
  service_principal_secret = var.service_principal_secret
}

module "k8s" {
  source                 = "./modules/k8s/"
  host                   = module.azurerm.host
  client_certificate     = base64decode(module.azurerm.client_certificate)
  client_key             = base64decode(module.azurerm.client_key)
  cluster_ca_certificate = base64decode(module.azurerm.cluster_ca_certificate)
  mssql_server           = module.azurerm.mssql_server
  mssql_server_admin     = module.azurerm.mssql_server_admin
  mssql_server_password  = module.azurerm.mssql_server_password
  rabbitmq_username      = var.rabbitmq_username
  rabbitmq_password      = var.rabbitmq_password
}
