variable "environment" {
  type    = string
  default = "Development"
}

variable "imported_container_registry_scope" {
  type = string
}

variable "location" {
  type    = string
  default = "canadacentral"
}

variable "prefix" {
  type    = string
  default = "dev"
}

variable "ssh_public_key" {
  type    = string
  default = "ssh-public-key.pub"
}
