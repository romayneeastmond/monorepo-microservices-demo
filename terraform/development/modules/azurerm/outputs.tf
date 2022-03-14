output "client_key" {
  value = azurerm_kubernetes_cluster.main_kubernetes_cluster.kube_config.0.client_key
}

output "client_certificate" {
  value = azurerm_kubernetes_cluster.main_kubernetes_cluster.kube_config.0.client_certificate
}

output "cluster_ca_certificate" {
  value = azurerm_kubernetes_cluster.main_kubernetes_cluster.kube_config.0.cluster_ca_certificate
}

output "cluster_username" {
  value = azurerm_kubernetes_cluster.main_kubernetes_cluster.kube_config.0.username
}

output "cluster_password" {
  value = azurerm_kubernetes_cluster.main_kubernetes_cluster.kube_config.0.password
}

output "kube_config" {
  value     = azurerm_kubernetes_cluster.main_kubernetes_cluster.kube_config_raw
  sensitive = true
}

output "host" {
  value = azurerm_kubernetes_cluster.main_kubernetes_cluster.kube_config.0.host
}

output "mssql_server" {
  value = azurerm_mssql_server.main_mssql_server.name
}

output "mssql_server_admin" {
  value = azurerm_mssql_server.main_mssql_server.administrator_login
}

output "mssql_server_password" {
  value     = azurerm_mssql_server.main_mssql_server.administrator_login_password
  sensitive = true
}
