## How to Use

To directly run terraform commands without using a service principal then login to Azure via the CLI using any of the following commands. Note that for accounts with multiple tenants, then the login should be scoped to a paritcular tenant id.

```
az login
```

```
az login --tenant TENANT_ID
```

#

## Terraform Specific Commands

To initialize, update configuration, or update providers then use the following

```
terraform init
```

To see what changes will be made using terraform, use

```
terraform plan
```

To apply the changes outlined within a configuration, use

```
terraform apply
```

To import an existing Azure resource to be managed by terraform, use

```
terraform import azurerm_TYPE_OF_RESOURCE.NAME_OF_RESOURCE_IN_CONFIGURATION AZURE_RESOURCE_ID
```

To view all resources currently managed in the terraform state, use

```
terraform state list
```

To remove a managed terraform resouce from the state, use

```
terraform state rm TYPE_OF_RESOURCE.NAME_OF_RESOURCE_IN_CONFIGURATION
```

To delete all resources that are currently managed by terraform, use

```
terraform destroy
```

To use any command within terraform with a predefined variables list file, use

```
terraform plan -var-file="SECRET_FILE_NAME.tfvars"
terraform apply -var-file="SECRET_FILE_NAME.tfvars"
terraform destroy -var-file="SECRET_FILE_NAME.tfvars"
```

#

## Azure CLI Commands

To list all subscriptions within a particular Azure accout use

```
az account list -o table
```

Alternatively to scope to a particular subscription instead of a tenant, use

```
az account set -s SUBSCRIPTION_ID
```

To generate a service principal, use

```
az ad sp create-for-rbac --name "SERVICE_PRINCIPAL_NAME"
```

To scope to the current Azure Kubernetes Services (AKS) instance to run kubectl commands use

```
az aks get-credentials --name AKS_INSTANCE_NAME --resource-group RESOURCE_GROUP_WHERE_AKS_INSTANCE_IS
```

#

## Additional Commands

To generate a ssh key for logging into sessions used by Azure resources use

```
ssh-keygen -m PEM -t rsa -b 4096
```
