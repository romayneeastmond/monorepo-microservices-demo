provider "azurerm" {
  subscription_id = var.subscription_id
  client_id       = var.service_principal
  client_secret   = var.service_principal_secret
  tenant_id       = var.tenant_id

  features {

  }
}

module "azurerm" {
  source = "./modules/azurerm/"
}
