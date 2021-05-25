# Ed-Fi-Roster-Sample-Application

Ed-Fi Roster Sample Application. This repository is to be used in conjunction with the [Ed-Fi Roster Sample Application](https://techdocs.ed-fi.org/display/SK/Ed-Fi+Roster+Sample+Application) page in the [Ed-Fi Tech Docs](https://techdocs.ed-fi.org).

## Prerequisites
The prerequisites for opening and running this solution are as follows:

* Visual Studio 2019 Community
* .NET Core 3.1
    * This is for the other projects in the solution

### OpenAPI Generator

The [OpenApi Generator](https://openapi-generator.tech/) is used to generate the SDK project in this repository.
Following were the steps followed to generate the SDK project:

- Install the openapi-generator JAR file using [wget](http://gnuwin32.sourceforge.net/packages/wget.htm) or Invoke-WebRequest in PowerShell (3.0+), e.g.
    ```
    Invoke-WebRequest -OutFile openapi-generator-cli.jar https://repo1.maven.org/maven2/org/openapitools/openapi-generator-cli/5.1.1/openapi-generator-cli-5.1.1.jar
    ```
- Use the following command to generate the SDK project:
    ```
    java -jar openapi-generator-cli.jar generate -g csharp-netcore -i https://api.ed-fi.org/v5.2/api/metadata/composites/v1/ed-fi/enrollment/swagger.json --api-package Api.EnrollmentComposites --model-package Models.EnrollmentComposites -o csharp-netcore --additional-properties=netCoreProjectFile --additional-properties=packageName='EdFi.Roster.Sdk' --additional-properties=targetFramework=netcoreapp3.1 --additional-properties=modelPropertyNaming=PascalCase
    ```
    **Note we are using the "csharp-netcore" generator here, and that generator-specific arguments are specified with the pattern "--additional-properties=NAME=VALUE". See https://openapi-generator.tech/docs/generators/csharp-netcore/ for a list of all such options. The output directory used above is csharp-netcore as well. **  
- This generates a whole solution including an empty tests project. However, only a few elements are needed for the final SDK project:
    - The .csproj file contents (for nuget package references).
    - The contents of "Api.EnrollmentComposites" folder
    - The contents of "Client" folder
    - The contents  of "Models.EnrollmentComposites" folder

## Legal Information

Copyright (c) 2020 Ed-Fi Alliance, LLC and contributors.

Licensed under the [Apache License, Version 2.0](LICENSE) (the "License").

Unless required by applicable law or agreed to in writing, software distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
CONDITIONS OF ANY KIND, either express or implied. See the License for the
specific language governing permissions and limitations under the License.

See [NOTICES](NOTICES.md) for additional copyright and license notifications.
