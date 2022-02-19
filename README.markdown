[![Build Microservices and Catalogue](https://github.com/romayneeastmond/monorepo-microservices-demo/actions/workflows/monorepo-microservices-build.yml/badge.svg?branch=main)](https://github.com/romayneeastmond/monorepo-microservices-demo/actions/workflows/monorepo-microservices-build.yml)

# Monorepo for Company Microservices

A sandbox environment for experimenting with CI/CD inside a monorepo. Each project uses a Docker container, .NET Core 6.0, and EntityFramework Core 6.0. They are microservices responsible for individual Api endpoints and database storage. Each Api is described using SwaggerUI.

- Company.Course
- Company.Department
- Company.Employee
- Company.Notification

A simple React application that links to each available Swagger endpoint within the microservices architecture.

- Microservices.Catalogue

## Company.Course

Responsible for providing employees with a list of training courses that they can enroll into.

## Company.Department

Groups employees into logical departments.

## Company.Employee

Defines the employees of the company.

## Company.Notification

Notifies the employees of any new training courses or to send reminders of courses that they are enrolled into.

## Microservices.Catalogue

A developer tool that lists all the available SwaggerUI endpoints.

## Copyright and Ownership

All terms used are copyright to their original authors.
