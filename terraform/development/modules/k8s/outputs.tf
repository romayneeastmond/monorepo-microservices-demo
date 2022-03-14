output "main_loadbalancer_company_course_ip" {
  value = kubernetes_service.main_loadbalancer_company_course.status.0.load_balancer.0.ingress.0.ip
}

output "main_loadbalancer_company_department_ip" {
  value = kubernetes_service.main_loadbalancer_company_department.status.0.load_balancer.0.ingress.0.ip
}

output "main_loadbalancer_company_employee_ip" {
  value = kubernetes_service.main_loadbalancer_company_employee.status.0.load_balancer.0.ingress.0.ip
}

output "main_loadbalancer_company_notification_ip" {
  value = kubernetes_service.main_loadbalancer_company_notification.status.0.load_balancer.0.ingress.0.ip
}

