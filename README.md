# Instructions

## Working with 'RocketPlayer'

### How to create image locally

```batch
# Build container image
docker build -t rocketplayer:latest .

# Run container using command
docker run --env "Url=http://yourserver/GameHub" rocketplayer:latest
``` 

### How to deploy to Azure Container Instances (ACI)

Deploy published image to [Azure Container Instances (ACI)](https://docs.microsoft.com/en-us/azure/container-instances/) the Azure CLI way:

```batch
# Variables
aciName="rocketplayer"
resourceGroup="rocketplayer-dev-rg"
location="westeurope"
image="jannemattila/rocketplayer:latest"
server="http://yourserver/GameHub"

# Login to Azure
az login

# *Explicitly* select your working context
az account set --subscription <YourSubscriptionName>

# Create new resource group
az group create --name $resourceGroup --location $location

# Create ACI
az container create --name $aciName --image $image --resource-group $resourceGroup -e Url=$server

# Show the properties
az container show --name $aciName --resource-group $resourceGroup

# Show the logs
az container logs --name $aciName --resource-group $resourceGroup

# Wipe out the resources
az group delete --name $resourceGroup -y
``` 

Deploy published image to [Azure Container Instances (ACI)](https://docs.microsoft.com/en-us/azure/container-instances/) the Azure PowerShell way:

```powershell
# Variables
$aciName="rocketplayer"
$resourceGroup="rocketplayer-dev-rg"
$location="westeurope"
$image="jannemattila/rocketplayer:latest"
$server="http://yourserver/GameHub"

# Login to Azure
Login-AzureRmAccount

# *Explicitly* select your working context
Select-AzureRmSubscription -SubscriptionName <YourSubscriptionName>

# Create new resource group
New-AzureRmResourceGroup -Name $resourceGroup -Location $location

# Create ACI
New-AzureRmContainerGroup -Name $aciName -Image $image -ResourceGroupName $resourceGroup -EnvironmentVariable @{"Url"=$server}

# Show the properties
Get-AzureRmContainerGroup -Name $aciName -ResourceGroupName $resourceGroup

# Show the logs
Get-AzureRmContainerInstanceLog -ContainerGroupName $aciName -ResourceGroupName $resourceGroup

# Wipe out the resources
Remove-AzureRmResourceGroup -Name $resourceGroup -Force
```

### How to deploy to Azure Container Services (AKS)

Deploy published image to [Azure Container Services (AKS)](https://docs.microsoft.com/en-us/azure/aks/):

Create `rocketplayer.yaml`:

```yaml
apiVersion: extensions/v1beta1
kind: Deployment
metadata:
  name: rocketplayer
  namespace: rocket
spec:
  replicas: 1
  template:
    metadata:
      labels:
        app: rocketplayer
    spec:
      containers:
      - image: jannemattila/rocketplayer:latest
        name: rocketplayer
        env:
          - name: APPLICATION_INSIGHTS_IKEY
            value: ""
```

```batch
kubectl apply -f rocketplayer.yaml
```
