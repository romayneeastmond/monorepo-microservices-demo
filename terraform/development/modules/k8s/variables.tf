variable "host" {
  type = string
}

variable "client_certificate" {
  type = string
}

variable "client_key" {
  type = string
}

variable "cluster_ca_certificate" {
  type = string
}

variable "mssql_server_admin" {
  type = string
}

variable "mssql_server" {
  type = string
}

variable "mssql_server_password" {
  type = string
}

variable "prefix" {
  type    = string
  default = "dev"
}

variable "rabbitmq_password" {
  type = string
}

variable "rabbitmq_username" {
  type = string
}
