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
