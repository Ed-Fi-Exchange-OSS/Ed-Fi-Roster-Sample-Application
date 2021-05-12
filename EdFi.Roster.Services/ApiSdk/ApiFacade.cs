using System;
using EdFi.Roster.Sdk.Client;
using EdFi.Roster.Sdk.Services;

namespace EdFi.Roster.Services.ApiSdk
{
    public class ApiFacade
    {
        private readonly Configuration configuration;

        public ApiFacade()
        {
            var settingsService = new ApiSettingsService();
            var apiSettings = settingsService.Read();
            var url = ApiFacade.Combine(apiSettings.RootUrl, "composites/v1");
            var tokenService = new BearerTokenService();
            var accessToken = tokenService.GetBearerToken();
            
            configuration = new Configuration
            {
                AccessToken = accessToken,
                BasePath = url
            };
        }

        public EdFi.Roster.Sdk.Api.EnrollmentComposites.ILocalEducationAgenciesApi LocalEducationAgenciesApi
        {
            get
            {
                return new EdFi.Roster.Sdk.Api.EnrollmentComposites.LocalEducationAgenciesApi(this.configuration);
            }
        }

        public EdFi.Roster.Sdk.Api.EnrollmentComposites.ISchoolsApi SchoolsApi
        {
            get
            {
                return new EdFi.Roster.Sdk.Api.EnrollmentComposites.SchoolsApi(this.configuration);
            }
        }

        public EdFi.Roster.Sdk.Api.EnrollmentComposites.IStaffsApi StaffsApi
        {
            get
            {
                return new EdFi.Roster.Sdk.Api.EnrollmentComposites.StaffsApi(this.configuration);
            }
        }

        public EdFi.Roster.Sdk.Api.EnrollmentComposites.IStudentsApi StudentsApi
        {
            get
            {
                return new EdFi.Roster.Sdk.Api.EnrollmentComposites.StudentsApi(this.configuration);
            }
        }

        public EdFi.Roster.Sdk.Api.EnrollmentComposites.ISectionsApi SectionsApi
        {
            get
            {
                return new EdFi.Roster.Sdk.Api.EnrollmentComposites.SectionsApi(this.configuration);
            }
        }

        /// <summary>
        /// Combines a path and a relative path.
        /// https://github.com/OfficeDev/PnP-Sites-Core/blob/master/Core/OfficeDevPnP.Core/Utilities/UrlUtility.cs
        /// </summary>
        /// <param name="path"></param>
        /// <param name="relative"></param>
        /// <returns></returns>
        private static string Combine(string path, string relative)
        {
            const char PATH_DELIMITER = '/';
            if (relative == null)
                relative = String.Empty;

            if (path == null)
                path = String.Empty;

            if (relative.Length == 0 && path.Length == 0)
                return String.Empty;

            if (relative.Length == 0)
                return path;

            if (path.Length == 0)
                return relative;

            path = path.Replace('\\', PATH_DELIMITER);
            relative = relative.Replace('\\', PATH_DELIMITER);

            var ret = path.TrimEnd(PATH_DELIMITER) + PATH_DELIMITER + relative.TrimStart(PATH_DELIMITER);
            return ret;
        }
    }
}
