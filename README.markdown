[![Build Microservices and Catalogue](https://github.com/romayneeastmond/monorepo-microservices-demo/actions/workflows/monorepo-microservices-build.yml/badge.svg?branch=main)](https://github.com/romayneeastmond/monorepo-microservices-demo/actions/workflows/monorepo-microservices-build.yml)

# Monorepo for Company Microservices

A sandbox environment for experimenting with CI/CD inside a monorepo. Each project uses a Docker container, .NET Core 6.0, and EntityFramework Core 6.0. They are microservices responsible for individual Api endpoints and database storage. Each Api is described using SwaggerUI.

## Monorepo and Microservices Development Progress

- [x] Create monorepo directory structure
- [x] Define .NET 6.0 microservices projects
- [x] Create Docker configuration for containers
- [x] Use Docker Compose for dev environment
- [x] Define minimal Api structure
- [x] Create SwaggerUI Api placeholder definitions
- [x] Create React microservices catalogue project
- [x] Install RabbitMQ / MassTransit broker for EventBus
- [x] Develop databases, migrations, and seed data
- [x] Optionally deploy microservices to Azure Web Apps
- [x] Create Azure Kubernetes Services (AKS) orchestration with Terraform
- [x] Publish Docker containers to Azure Container Registry (ACR)
- [x] Publish Docker containers to Azure Container Registry (ACR) with AzureDevOps
- [x] Optionally deploy containers to Azure Kubernetes Services (AKS) with AzureDevOps
- [x] Optionally deploy containers to Azure Kubernetes Services (AKS)
- [x] Create Apollo GraphQL with Swagger Apis as REST data sources
- [ ] Develop Unit Testing strategies

#

### Microservices Api

- Company.Course
- Company.Department
- Company.Employee
- Company.Notification

### Microservices Services

- Microservices.Catalogue
- Microservices.EventBus

## Company.Course

Responsible for providing employees with a list of training courses that they can enroll into.

## Company.Department

Groups employees into logical departments.

## Company.Employee

Defines the employees of the company.

## Company.Notification

Notifies the employees of any new training courses or to send reminders of courses that they are enrolled into.

## Microservices.Catalogue

A React application that is a developer tool for listing all the available SwaggerUI endpoints.

## Microservices.EventBus

A RabbitMQ / MassTransit event layer that creates messages that are produced and consumed between the microservices.

### Examples of Event Queues

| Queue                     | Producer             | Consumer             | Description                                         |
| ------------------------- | -------------------- | -------------------- | --------------------------------------------------- |
| course-created            | Company.Course       | Company.Notification | Prepare a notification email list of employees      |
| department-deleted        | Company.Department   | Company.Employee     | Uncategorize employees from a deleted department    |
| employee-created          | Company.Employee     | Company.Notification | Sends a welcome email to a newly created employee   |
| employee-course-broadcast | Company.Employee     | Company.Notification | Sends an email with details of a new course         |
| notification-list         | Company.Notification | Company.Employee     | Asks for the employee email addresses for broadcast |

## Copyright and Ownership

All terms used are copyright to their original authors.
