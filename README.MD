# Overview 

DotNET SDK for the [Fusion API](https://datameshgroup.github.io/fusion)

# Publish

Package contains a GitHub action (`build-and-nuget-publish.yml`) which builds and deploys the Nuget packages:

- [DataMeshGroup.Fusion.FusionClient](https://www.nuget.org/packages/DataMeshGroup.Fusion.FusionClient)
- [DataMeshGroup.Fusion.Model](https://www.nuget.org/packages/DataMeshGroup.Fusion.Model)

## Publish secret

Nuget deployment has a dependency on the `PUBLISH_NUGET_SECRET` secret. 

To update the secret

- create an [API key on Nuget](https://www.nuget.org/account/apikeys) which has access to the `DataMeshGroup.Fusion.FusionClient` and `DataMeshGroup.Fusion.Model` packages
- Update the `PUBLISH_NUGET_SECRET` under the [Github secrets](https://github.com/organizations/datameshgroup/settings/secrets/actions)
