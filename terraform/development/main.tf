provider "azurerm" {
  features {

  }
}

provider "kubernetes" {
  features {

  }
}

variable "imported_container_registry_scope" {
  type = string
}

module "azurerm" {
  source                            = "./modules/azurerm/"
  imported_container_registry_scope = var.imported_container_registry_scope
}

module "k8s" {
  source                 = "./modules/k8s/"
  host                   = module.azurerm.host
  client_certificate     = base64decode(module.azurerm.client_certificate)
  client_key             = base64decode(module.azurerm.client_key)
  cluster_ca_certificate = base64decode(module.azurerm.cluster_ca_certificate)
}
