terraform {
  required_providers {
    kubernetes = {
      source = "hashicorp/kubernetes"
    }
  }
}

provider "kubernetes" {
  host                   = var.host
  client_certificate     = var.client_certificate
  client_key             = var.client_key
  cluster_ca_certificate = var.cluster_ca_certificate
}

resource "kubernetes_pod" "main_pod_rabbitmq" {
  metadata {
    name = "rabbitmq"
    labels = {
      app = "main_pod_company_course"
    }
  }

  spec {
    container {
      image = "rabbitmq:3-management-alpine"
      name  = "rabbitmq"

      port {
        container_port = 5672
      }

      port {
        container_port = 15672
      }
    }
  }
}

resource "kubernetes_service" "main_loadbalancer_rabbitmq_01" {
  metadata {
    name = "rabbitmq-load-balancer-01"
  }
  spec {
    selector = {
      app = kubernetes_pod.main_pod_rabbitmq.metadata.0.labels.app
    }

    port {
      port        = 5672
      target_port = 5672
      node_port   = 31006
    }

    type = "LoadBalancer"
  }
}

resource "kubernetes_service" "main_loadbalancer_rabbitmq_02" {
  metadata {
    name = "rabbitmq-load-balancer-02"
  }
  spec {
    selector = {
      app = kubernetes_pod.main_pod_rabbitmq.metadata.0.labels.app
    }

    port {
      port        = 80
      target_port = 15672
      node_port   = 31007
    }

    type = "LoadBalancer"
  }
}

resource "kubernetes_pod" "main_pod_company_course" {
  metadata {
    name = "company-course"
    labels = {
      app = "main_pod_company_course"
    }
  }

  spec {
    container {
      image = "devcontainerregistryre02.azurecr.io/company-course:ci-1.0.1"
      name  = "company-course"

      port {
        container_port = 80
      }

      port {
        container_port = 443
      }

      env {
        name  = "ASPNETCORE_ENVIRONMENT"
        value = "Development"
      }

      env {
        name  = "ConnectionStrings__MicroserviceDbString"
        value = "Data Source=tcp:${var.mssql_server}.database.windows.net,1433;Initial Catalog=CompanyMicroservicesCourses;User Id=${var.mssql_server_admin}@${var.mssql_server};Password=${var.mssql_server_password}"
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQServer"
        value = kubernetes_service.main_loadbalancer_rabbitmq_01.status.0.load_balancer.0.ingress.0.ip
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQUsername"
        value = var.rabbitmq_username
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQPassword"
        value = var.rabbitmq_password
      }
    }
  }
}


resource "kubernetes_service" "main_loadbalancer_company_course" {
  metadata {
    name = "company-course-load-balancer"
  }
  spec {
    selector = {
      app = kubernetes_pod.main_pod_company_course.metadata.0.labels.app
    }

    port {
      port        = 80
      target_port = 80
      node_port   = 31001
    }

    type = "LoadBalancer"
  }
}

resource "kubernetes_pod" "main_pod_company_department" {
  metadata {
    name = "company-department"
    labels = {
      app = "main_pod_company_department"
    }
  }

  spec {
    container {
      image = "devcontainerregistryre02.azurecr.io/company-department:ci-1.0.1"
      name  = "company-department"

      port {
        container_port = 80
      }

      port {
        container_port = 443
      }

      env {
        name  = "ASPNETCORE_ENVIRONMENT"
        value = "Development"
      }

      env {
        name  = "ConnectionStrings__MicroserviceDbString"
        value = "Data Source=tcp:${var.mssql_server}.database.windows.net,1433;Initial Catalog=CompanyMicroservicesDepartments;User Id=${var.mssql_server_admin}@${var.mssql_server};Password=${var.mssql_server_password}"
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQServer"
        value = kubernetes_service.main_loadbalancer_rabbitmq_01.status.0.load_balancer.0.ingress.0.ip
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQUsername"
        value = var.rabbitmq_username
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQPassword"
        value = var.rabbitmq_password
      }
    }
  }
}

resource "kubernetes_service" "main_loadbalancer_company_department" {
  metadata {
    name = "company-department-load-balancer"
  }
  spec {
    selector = {
      app = kubernetes_pod.main_pod_company_department.metadata.0.labels.app
    }

    port {
      port        = 80
      target_port = 80
      node_port   = 31002
    }

    type = "LoadBalancer"
  }
}

resource "kubernetes_pod" "main_pod_company_employee" {
  metadata {
    name = "company-employee"
    labels = {
      app = "main_pod_company_employee"
    }
  }

  spec {
    container {
      image = "devcontainerregistryre02.azurecr.io/company-employee:ci-1.0.1"
      name  = "company-employee"

      port {
        container_port = 80
      }

      port {
        container_port = 443
      }

      env {
        name  = "ASPNETCORE_ENVIRONMENT"
        value = "Development"
      }

      env {
        name  = "ConnectionStrings__MicroserviceDbString"
        value = "Data Source=tcp:${var.mssql_server}.database.windows.net,1433;Initial Catalog=CompanyMicroservicesEmployees;User Id=${var.mssql_server_admin}@${var.mssql_server};Password=${var.mssql_server_password}"
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQServer"
        value = kubernetes_service.main_loadbalancer_rabbitmq_01.status.0.load_balancer.0.ingress.0.ip
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQUsername"
        value = var.rabbitmq_username
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQPassword"
        value = var.rabbitmq_password
      }
    }
  }
}

resource "kubernetes_service" "main_loadbalancer_company_employee" {
  metadata {
    name = "company-employee-load-balancer"
  }
  spec {
    selector = {
      app = kubernetes_pod.main_pod_company_employee.metadata.0.labels.app
    }

    port {
      port        = 80
      target_port = 80
      node_port   = 31003
    }

    type = "LoadBalancer"
  }
}

resource "kubernetes_pod" "main_pod_company_notification" {
  metadata {
    name = "company-notification"
    labels = {
      app = "main_pod_company_notification"
    }
  }

  spec {
    container {
      image = "devcontainerregistryre02.azurecr.io/company-notification:ci-1.0.1"
      name  = "company-notification"

      port {
        container_port = 80
      }

      port {
        container_port = 443
      }

      env {
        name  = "ASPNETCORE_ENVIRONMENT"
        value = "Development"
      }

      env {
        name  = "ConnectionStrings__MicroserviceDbString"
        value = "Data Source=tcp:${var.mssql_server}.database.windows.net,1433;Initial Catalog=CompanyMicroservicesNotifications;User Id=${var.mssql_server_admin}@${var.mssql_server};Password=${var.mssql_server_password}"
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQServer"
        value = kubernetes_service.main_loadbalancer_rabbitmq_01.status.0.load_balancer.0.ingress.0.ip
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQUsername"
        value = var.rabbitmq_username
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQPassword"
        value = var.rabbitmq_password
      }
    }
  }
}

resource "kubernetes_service" "main_loadbalancer_company_notification" {
  metadata {
    name = "company-notification-load-balancer"
  }
  spec {
    selector = {
      app = kubernetes_pod.main_pod_company_notification.metadata.0.labels.app
    }

    port {
      port        = 80
      target_port = 80
      node_port   = 31004
    }

    type = "LoadBalancer"
  }
}

resource "kubernetes_pod" "main_pod_microservices_catalogue" {
  metadata {
    name = "microservices-catalogue"
    labels = {
      app = "main_pod_microservices_catalogue"
    }
  }

  spec {
    container {
      image = "devcontainerregistryre02.azurecr.io/microservices-catalogue:ci-1.0.1"
      name  = "microservices-catalogue"

      port {
        container_port = 3000
      }

      env {
        name  = "REACT_APP_COMPANY_COURSE"
        value = "http://${kubernetes_service.main_loadbalancer_company_course.status.0.load_balancer.0.ingress.0.ip}/swagger/index.html"
      }

      env {
        name  = "REACT_APP_COMPANY_DEPARTMENT"
        value = "http://${kubernetes_service.main_loadbalancer_company_department.status.0.load_balancer.0.ingress.0.ip}/swagger/index.html"
      }

      env {
        name  = "REACT_APP_COMPANY_EMPLOYEE"
        value = "http://${kubernetes_service.main_loadbalancer_company_employee.status.0.load_balancer.0.ingress.0.ip}/swagger/index.html"
      }

      env {
        name  = "REACT_APP_COMPANY_NOTIFICATION"
        value = "http://${kubernetes_service.main_loadbalancer_company_notification.status.0.load_balancer.0.ingress.0.ip}/swagger/index.html"
      }

      env {
        name  = "REACT_APP_RABBITMQ"
        value = "http://${kubernetes_service.main_loadbalancer_rabbitmq_02.status.0.load_balancer.0.ingress.0.ip}"
      }
    }
  }
}

resource "kubernetes_service" "main_loadbalancer_microservices_catalogue" {
  metadata {
    name = "microservices-catalogue-load-balancer"
  }

  spec {
    selector = {
      app = kubernetes_pod.main_pod_microservices_catalogue.metadata.0.labels.app
    }

    port {
      port        = 80
      target_port = 3000
      node_port   = 31005
    }

    type = "LoadBalancer"
  }
}
