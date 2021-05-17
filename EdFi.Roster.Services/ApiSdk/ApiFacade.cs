using EdFi.Roster.Sdk.Client;

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

        public Sdk.Api.EnrollmentComposites.ILocalEducationAgenciesApi LocalEducationAgenciesApi => 
            new Sdk.Api.EnrollmentComposites.LocalEducationAgenciesApi(configuration);

        public Sdk.Api.EnrollmentComposites.ISchoolsApi SchoolsApi => 
            new Sdk.Api.EnrollmentComposites.SchoolsApi(configuration);

        public Sdk.Api.EnrollmentComposites.IStaffsApi StaffsApi => 
            new Sdk.Api.EnrollmentComposites.StaffsApi(configuration);

        public Sdk.Api.EnrollmentComposites.IStudentsApi StudentsApi => 
            new Sdk.Api.EnrollmentComposites.StudentsApi(configuration);

        public Sdk.Api.EnrollmentComposites.ISectionsApi SectionsApi => 
            new Sdk.Api.EnrollmentComposites.SectionsApi(configuration);

        /// <summary>
        /// Combines a path and a relative path.
        /// https://github.com/OfficeDev/PnP-Sites-Core/blob/master/Core/OfficeDevPnP.Core/Utilities/UrlUtility.cs
        /// </summary>
        /// <param name="path"></param>
        /// <param name="relative"></param>
        /// <returns></returns>
        private static string Combine(string path, string relative)
        {
            const char pathDelimiter = '/';
            relative ??= string.Empty;

            path ??= string.Empty;

            if (relative.Length == 0 && path.Length == 0)
                return string.Empty;

            if (relative.Length == 0)
                return path;

            if (path.Length == 0)
                return relative;

            path = path.Replace('\\', pathDelimiter);
            relative = relative.Replace('\\', pathDelimiter);

            var ret = path.TrimEnd(pathDelimiter) + pathDelimiter + relative.TrimStart(pathDelimiter);
            return ret;
        }
    }
}
