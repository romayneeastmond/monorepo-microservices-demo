# terraform {
#   required_providers {
#     kubernetes = {
#       source = "hashicorp/kubernetes"
#     }
#   }
# }

# provider "kubernetes" {
#   host                   = var.host
#   client_certificate     = var.client_certificate
#   client_key             = var.client_key
#   cluster_ca_certificate = var.cluster_ca_certificate
# }

# resource "kubernetes_deployment" "main_deployment_company_course" {
#   metadata {
#     name = "company-course-deployment"
#     labels = {
#       app = "main_deployment_company_course"
#     }
#   }

#   spec {
#     replicas = 2

#     selector {
#       match_labels = {
#         app = "main_deployment_company_course"
#       }
#     }

#     template {
#       metadata {
#         labels = {
#           app = "main_deployment_company_course"
#         }
#       }

#       spec {
#         container {
#           image = "devcontainerregistryre02.azurecr.io/company-course:ci-1.0.1"
#           name  = "company-course"

#           port {
#             container_port = 80
#           }

#           port {
#             container_port = 443
#           }

#           resources {
#             requests = {
#               cpu    = "250m"
#               memory = "64Mi"
#             }

#             limits = {
#               cpu    = "500m"
#               memory = "256Mi"
#             }
#           }

#           liveness_probe {
#             http_get {
#               path = "/"
#               port = 80
#             }

#             initial_delay_seconds = 60
#             period_seconds        = 30
#           }
#         }
#       }
#     }
#   }
# }

# resource "kubernetes_service" "main_loadbalancer_company_course" {
#   metadata {
#     name = "company-course-load-balancer"
#   }

#   spec {
#     selector = {
#       app = kubernetes_deployment.main_deployment_company_course.metadata.0.labels.app
#     }

#     port {
#       port        = 80
#       target_port = 80
#       node_port   = 31001
#     }

#     type = "LoadBalancer"
#   }
# }

# resource "kubernetes_deployment" "main_deployment_company_department" {
#   metadata {
#     name = "company-department-deployment"
#     labels = {
#       app = "main_deployment_company_department"
#     }
#   }

#   spec {
#     replicas = 2

#     selector {
#       match_labels = {
#         app = "main_deployment_company_department"
#       }
#     }

#     template {
#       metadata {
#         labels = {
#           app = "main_deployment_company_department"
#         }
#       }

#       spec {
#         container {
#           image = "devcontainerregistryre02.azurecr.io/company-department:ci-1.0.1"
#           name  = "company-department"

#           port {
#             container_port = 80
#           }

#           port {
#             container_port = 443
#           }

#           resources {
#             requests = {
#               cpu    = "250m"
#               memory = "64Mi"
#             }

#             limits = {
#               cpu    = "500m"
#               memory = "256Mi"
#             }
#           }

#           liveness_probe {
#             http_get {
#               path = "/"
#               port = 80
#             }

#             initial_delay_seconds = 60
#             period_seconds        = 30
#           }
#         }
#       }
#     }
#   }
# }

# resource "kubernetes_service" "main_loadbalancer_company_department" {
#   metadata {
#     name = "company-department-load-balancer"
#   }
#   spec {
#     selector = {
#       app = kubernetes_deployment.main_deployment_company_department.metadata.0.labels.app
#     }

#     port {
#       port        = 80
#       target_port = 80
#       node_port   = 31002
#     }

#     type = "LoadBalancer"
#   }
# }

# resource "kubernetes_deployment" "main_deployment_company_employee" {
#   metadata {
#     name = "company-employee-deployment"
#     labels = {
#       app = "main_deployment_company_employee"
#     }
#   }

#   spec {
#     replicas = 2

#     selector {
#       match_labels = {
#         app = "main_deployment_company_employee"
#       }
#     }

#     template {
#       metadata {
#         labels = {
#           app = "main_deployment_company_employee"
#         }
#       }

#       spec {
#         container {
#           image = "devcontainerregistryre02.azurecr.io/company-employee:ci-1.0.1"
#           name  = "company-employee"

#           port {
#             container_port = 80
#           }

#           port {
#             container_port = 443
#           }

#           resources {
#             requests = {
#               cpu    = "250m"
#               memory = "64Mi"
#             }

#             limits = {
#               cpu    = "500m"
#               memory = "256Mi"
#             }
#           }

#           liveness_probe {
#             http_get {
#               path = "/"
#               port = 80
#             }

#             initial_delay_seconds = 60
#             period_seconds        = 30
#           }
#         }
#       }
#     }
#   }
# }

# resource "kubernetes_service" "main_loadbalancer_company_employee" {
#   metadata {
#     name = "company-employee-load-balancer"
#   }

#   spec {
#     selector = {
#       app = kubernetes_deployment.main_deployment_company_employee.metadata.0.labels.app
#     }

#     port {
#       port        = 80
#       target_port = 80
#       node_port   = 31003
#     }

#     type = "LoadBalancer"
#   }
# }


# resource "kubernetes_deployment" "main_deployment_company_notification" {
#   metadata {
#     name = "company-notification-deployment"
#     labels = {
#       app = "main_deployment_company_notification"
#     }
#   }

#   spec {
#     replicas = 2

#     selector {
#       match_labels = {
#         app = "main_deployment_company_notification"
#       }
#     }

#     template {
#       metadata {
#         labels = {
#           app = "main_deployment_company_notification"
#         }
#       }

#       spec {
#         container {
#           image = "devcontainerregistryre02.azurecr.io/company-notification:ci-1.0.1"
#           name  = "company-notification"

#           port {
#             container_port = 80
#           }

#           port {
#             container_port = 443
#           }

#           resources {
#             requests = {
#               cpu    = "250m"
#               memory = "64Mi"
#             }

#             limits = {
#               cpu    = "500m"
#               memory = "256Mi"
#             }
#           }

#           liveness_probe {
#             http_get {
#               path = "/"
#               port = 80
#             }

#             initial_delay_seconds = 60
#             period_seconds        = 30
#           }
#         }
#       }
#     }
#   }
# }

# resource "kubernetes_service" "main_loadbalancer_company_notification" {
#   metadata {
#     name = "company-notification-load-balancer"
#   }
#   spec {
#     selector = {
#       app = kubernetes_deployment.main_deployment_company_notification.metadata.0.labels.app
#     }

#     port {
#       port        = 80
#       target_port = 80
#       node_port   = 31004
#     }

#     type = "LoadBalancer"
#   }
# }

# resource "kubernetes_deployment" "main_deployment_microservices_catalogue" {
#   metadata {
#     name = "microservices-catalogue-deployment"
#     labels = {
#       app = "main_deployment_microservices_catalogue"
#     }
#   }

#   spec {
#     replicas = 2

#     selector {
#       match_labels = {
#         app = "main_deployment_microservices_catalogue"
#       }
#     }

#     template {
#       metadata {
#         labels = {
#           app = "main_deployment_microservices_catalogue"
#         }
#       }

#       spec {
#         container {
#           image = "devcontainerregistryre02.azurecr.io/microservices-catalogue:ci-1.0.1"
#           name  = "microservices-catalogue"

#           port {
#             container_port = 80
#           }

#           env {
#             name  = "REACT_APP_COMPANY_COURSE"
#             value = "http://${kubernetes_service.main_loadbalancer_company_course.status.0.load_balancer.0.ingress.0.ip}/swagger/index.html"
#           }

#           env {
#             name  = "REACT_APP_COMPANY_DEPARTMENT"
#             value = "http://${kubernetes_service.main_loadbalancer_company_department.status.0.load_balancer.0.ingress.0.ip}/swagger/index.html"
#           }

#           env {
#             name  = "REACT_APP_COMPANY_EMPLOYEE"
#             value = "http://${kubernetes_service.main_loadbalancer_company_employee.status.0.load_balancer.0.ingress.0.ip}/swagger/index.html"
#           }

#           env {
#             name  = "REACT_APP_COMPANY_NOTIFICATION"
#             value = "http://${kubernetes_service.main_loadbalancer_company_notification.status.0.load_balancer.0.ingress.0.ip}/swagger/index.html"
#           }

#           resources {
#             requests = {
#               cpu    = "250m"
#               memory = "64Mi"
#             }

#             limits = {
#               cpu    = "500m"
#               memory = "512Mi"
#             }
#           }

#           liveness_probe {
#             http_get {
#               path = "/"
#               port = 80
#             }

#             initial_delay_seconds = 180
#             period_seconds        = 30
#           }
#         }
#       }
#     }
#   }
# }

# resource "kubernetes_service" "main_loadbalancer_microservices_catalogue" {
#   metadata {
#     name = "microservices-catalogue-load-balancer"
#   }

#   spec {
#     selector = {
#       app = kubernetes_deployment.main_deployment_microservices_catalogue.metadata.0.labels.app
#     }

#     port {
#       port        = 80
#       target_port = 3000
#       node_port   = 31005
#     }

#     type = "LoadBalancer"
#   }
# }
