variable "environment" {
  type    = string
  default = "Development"
}

variable "location" {
  type    = string
  default = "canadacentral"
}

variable "prefix" {
  type    = string
  default = "dev"
}

variable "service_principal" {
  type = string
}

variable "service_principal_secret" {
  type = string
}

variable "ssh_public_key" {
  type    = string
  default = "ssh-public-key.pub"
}
