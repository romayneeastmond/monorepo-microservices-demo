## Kubernetes Commands

To list all pods, use

```
kubectl get pods -o wide
```

To list all deployments, use

```
kubectl get deployments -o wide
```

To list all services, use

```
kubectl get services -o wide
```

To run a pod image as an interactive shell, use

```
kubectl exec --stdin --tty POD_NAME -- /bin/sh
```

#

## Kubernetes Dashboard UI

The Kubernetes Dashboard UI can be accessed from http://localhost:8001/api/v1/namespaces/kubernetes-dashboard/services/https:kubernetes-dashboard:/proxy/ after it has been installed.

To install the Kubernetes Dashboard UI run the following commands

```
kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v2.5.0/aio/deploy/recommended.yaml
```

```
kubectl proxy
```

The default service account tokens can be found within the Azure Kubernetes Services cluster's Configuration > Secrets > kubernetes-dashboard-token\* under the 'Kubernetes resources' section.
