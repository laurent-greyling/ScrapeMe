function Set-ServiceBusResource {
    param (
        [string] $ResourceGroupName,
        [string] $ServiceBusName,
        [string] $QueueName
    )

    az login

    az group create -l westeurope -n $ResourceGroupName

    az servicebus namespace create --resource-group $ResourceGroupName --name $ServiceBusName --location westeurope

    az servicebus queue create --resource-group $ResourceGroupName  --namespace-name $ServiceBusName --name $QueueName
}

Export-ModuleMember -Function "Set-ServiceBusResource"