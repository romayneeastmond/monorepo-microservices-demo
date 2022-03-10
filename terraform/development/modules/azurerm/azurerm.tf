terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
    }
  }
}

resource "azurerm_resource_group" "main_resource_group" {
  name     = "${var.prefix}-kubernetes-cluster-re02-rg"
  location = var.location
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
    admin_username = "ubuntu"

    ssh_key {
      key_data = file(var.ssh_public_key)
    }
  }

  default_node_pool {
    name       = "default"
    node_count = 1
    vm_size    = "Standard_D2_v2"
  }

  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_role_assignment" "main_container_registry_pull" {
  principal_id                     = azurerm_kubernetes_cluster.main_kubernetes_cluster.kubelet_identity[0].object_id
  role_definition_name             = "AcrPull"
  scope                            = var.imported_container_registry_scope
  skip_service_principal_aad_check = true
}
