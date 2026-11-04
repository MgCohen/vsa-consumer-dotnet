# vsa-consumer-dotnet

A .NET app that uses the published VSA packages from [vsa-template](https://github.com/MgCohen/vsa-template). The `vsa` tool wrote it. It is the release check of VSA for .NET: CI restores, builds and tests the app with the packages from GitHub Packages.

## How the app was made

```bash
dotnet tool exec VSA.Cli@0.1.0-preview.2 -- new app Consumer
dotnet vsa add module Counter
dotnet vsa add request Increment -m Counter
```

`Tests/` is not from the tool. It has the VSA architecture tests and one unit test.

## Feed access

GitHub Packages needs a token, also for public packages. `nuget.config` reads it from `GITHUB_TOKEN`.

| Where | Token |
|---|---|
| GitHub Actions | `GITHUB_TOKEN` of the workflow |
| Dev machine | `gh auth token`, after `gh auth refresh -s read:packages` |

```bash
export GITHUB_TOKEN=$(gh auth token)
dotnet test Consumer.slnx
```

## Update VSA

1. Set the new version in `global.json` (`VSA.Sdk`), `dotnet-tools.json`, `App/Host/App.Host.csproj`, `App/Views/App.Views.csproj` and `Tests/Consumer.Tests.csproj`.
2. Run `dotnet vsa doctor`. It shows each version that is different.
3. Run the tests. If a release is new, use `dotnet restore --no-http-cache` one time.
