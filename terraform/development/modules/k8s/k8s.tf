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
      app = "main_pod_rabbitmq"
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

resource "kubernetes_config_map" "main_config_map_01" {
  metadata {
    name = "main-config-map-01"
  }

  data = {
    aspnetcore_environment                 = "Development"
    connection_string_company_course       = "Data Source=tcp:${var.mssql_server}.database.windows.net,1433;Initial Catalog=CompanyMicroservicesCourses;User Id=${var.mssql_server_admin}@${var.mssql_server};Password=${var.mssql_server_password}"
    connection_string_company_department   = "Data Source=tcp:${var.mssql_server}.database.windows.net,1433;Initial Catalog=CompanyMicroservicesDepartments;User Id=${var.mssql_server_admin}@${var.mssql_server};Password=${var.mssql_server_password}"
    connection_string_company_employee     = "Data Source=tcp:${var.mssql_server}.database.windows.net,1433;Initial Catalog=CompanyMicroservicesEmployees;User Id=${var.mssql_server_admin}@${var.mssql_server};Password=${var.mssql_server_password}"
    connection_string_company_notification = "Data Source=tcp:${var.mssql_server}.database.windows.net,1433;Initial Catalog=CompanyMicroservicesNotifications;User Id=${var.mssql_server_admin}@${var.mssql_server};Password=${var.mssql_server_password}"
    rabbitmq_server                        = kubernetes_service.main_loadbalancer_rabbitmq_01.status.0.load_balancer.0.ingress.0.ip
    rabbitmq_username                      = var.rabbitmq_username
    rabbitmq_password                      = var.rabbitmq_password
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
        value = kubernetes_config_map.main_config_map_01.data.aspnetcore_environment
      }

      env {
        name  = "ConnectionStrings__MicroserviceDbString"
        value = kubernetes_config_map.main_config_map_01.data.connection_string_company_course
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQServer"
        value = kubernetes_config_map.main_config_map_01.data.rabbitmq_server
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQUsername"
        value = kubernetes_config_map.main_config_map_01.data.rabbitmq_username
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQPassword"
        value = kubernetes_config_map.main_config_map_01.data.rabbitmq_password
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
        value = kubernetes_config_map.main_config_map_01.data.aspnetcore_environment
      }

      env {
        name  = "ConnectionStrings__MicroserviceDbString"
        value = kubernetes_config_map.main_config_map_01.data.connection_string_company_department
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQServer"
        value = kubernetes_config_map.main_config_map_01.data.rabbitmq_server
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQUsername"
        value = kubernetes_config_map.main_config_map_01.data.rabbitmq_username
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQPassword"
        value = kubernetes_config_map.main_config_map_01.data.rabbitmq_password
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
        value = kubernetes_config_map.main_config_map_01.data.aspnetcore_environment
      }

      env {
        name  = "ConnectionStrings__MicroserviceDbString"
        value = kubernetes_config_map.main_config_map_01.data.connection_string_company_employee
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQServer"
        value = kubernetes_config_map.main_config_map_01.data.rabbitmq_server
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQUsername"
        value = kubernetes_config_map.main_config_map_01.data.rabbitmq_username
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQPassword"
        value = kubernetes_config_map.main_config_map_01.data.rabbitmq_password
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
        value = kubernetes_config_map.main_config_map_01.data.aspnetcore_environment
      }

      env {
        name  = "ConnectionStrings__MicroserviceDbString"
        value = kubernetes_config_map.main_config_map_01.data.connection_string_company_notification
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQServer"
        value = kubernetes_config_map.main_config_map_01.data.rabbitmq_server
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQUsername"
        value = kubernetes_config_map.main_config_map_01.data.rabbitmq_username
      }

      env {
        name  = "RabbitMQConfiguration__RabbitMQPassword"
        value = kubernetes_config_map.main_config_map_01.data.rabbitmq_password
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

resource "kubernetes_config_map" "main_config_map_02" {
  metadata {
    name = "main-config-map-02"
  }

  data = {
    company_course                 = "http://${kubernetes_service.main_loadbalancer_company_course.status.0.load_balancer.0.ingress.0.ip}/"
    company_department             = "http://${kubernetes_service.main_loadbalancer_company_department.status.0.load_balancer.0.ingress.0.ip}/"
    company_employee               = "http://${kubernetes_service.main_loadbalancer_company_employee.status.0.load_balancer.0.ingress.0.ip}/"
    company_notification           = "http://${kubernetes_service.main_loadbalancer_company_notification.status.0.load_balancer.0.ingress.0.ip}/"
    react_app_company_course       = "http://${kubernetes_service.main_loadbalancer_company_course.status.0.load_balancer.0.ingress.0.ip}/swagger/index.html"
    react_app_company_department   = "http://${kubernetes_service.main_loadbalancer_company_department.status.0.load_balancer.0.ingress.0.ip}/swagger/index.html"
    react_app_company_employee     = "http://${kubernetes_service.main_loadbalancer_company_employee.status.0.load_balancer.0.ingress.0.ip}/swagger/index.html"
    react_app_company_notification = "http://${kubernetes_service.main_loadbalancer_company_notification.status.0.load_balancer.0.ingress.0.ip}/swagger/index.html"
    react_app_rabbitmq             = "http://${kubernetes_service.main_loadbalancer_rabbitmq_02.status.0.load_balancer.0.ingress.0.ip}"
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
        value = kubernetes_config_map.main_config_map_02.data.react_app_company_course
      }

      env {
        name  = "REACT_APP_COMPANY_DEPARTMENT"
        value = kubernetes_config_map.main_config_map_02.data.react_app_company_department
      }

      env {
        name  = "REACT_APP_COMPANY_EMPLOYEE"
        value = kubernetes_config_map.main_config_map_02.data.react_app_company_employee
      }

      env {
        name  = "REACT_APP_COMPANY_NOTIFICATION"
        value = kubernetes_config_map.main_config_map_02.data.react_app_company_notification
      }

      env {
        name  = "REACT_APP_RABBITMQ"
        value = kubernetes_config_map.main_config_map_02.data.react_app_rabbitmq
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


resource "kubernetes_pod" "main_pod_microservices_graphql" {
  metadata {
    name = "microservices-graphql"
    labels = {
      app = "main_pod_microservices_graphql"
    }
  }

  spec {
    container {
      image = "devcontainerregistryre02.azurecr.io/microservices-graphql:ci-1.0.1"
      name  = "microservices-graphql"

      port {
        container_port = 4000
      }

      env {
        name  = "NODE_TLS_REJECT_UNAUTHORIZED"
        value = "0"
      }

      env {
        name  = "COMPANY_COURSE"
        value = kubernetes_config_map.main_config_map_02.data.company_course
      }

      env {
        name  = "COMPANY_DEPARTMENT"
        value = kubernetes_config_map.main_config_map_02.data.company_department
      }

      env {
        name  = "COMPANY_EMPLOYEE"
        value = kubernetes_config_map.main_config_map_02.data.company_employee
      }

      env {
        name  = "COMPANY_NOTIFICATION"
        value = kubernetes_config_map.main_config_map_02.data.company_notification
      }
    }
  }
}

resource "kubernetes_service" "main_loadbalancer_microservices_graphql" {
  metadata {
    name = "microservices-graphql-load-balancer"
  }

  spec {
    selector = {
      app = kubernetes_pod.main_pod_microservices_graphql.metadata.0.labels.app
    }

    port {
      port        = 80
      target_port = 4000
      node_port   = 31006
    }

    type = "LoadBalancer"
  }
}
