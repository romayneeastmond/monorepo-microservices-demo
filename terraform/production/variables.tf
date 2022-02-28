variable "environment" {
  type    = string
  default = "Production"
}

variable "location" {
  type    = string
  default = "canadacentral"
}

variable "prefix" {
  type    = string
  default = "prod"
}

variable "ssh_public_key" {
  type    = string
  default = "ssh-public-key.pub"
}
