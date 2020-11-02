using System;

namespace EdFi.Roster.Services.ApiSdk
{
    public class ApiFacade
    {
        private readonly string url;

        public ApiFacade()
        {
            var settingsService = new ApiSettingsService();
            var apiSettings = settingsService.Read();
            url = ApiFacade.Combine(apiSettings.RootUrl, "composites/v1");
        }

        public EdFi.Roster.Sdk.Api.EnrollmentComposites.ILocalEducationAgenciesApi LocalEducationAgenciesApi
        {
            get
            {
                return new EdFi.Roster.Sdk.Api.EnrollmentComposites.LocalEducationAgenciesApi(this.url);
            }
        }

        public EdFi.Roster.Sdk.Api.EnrollmentComposites.ISchoolsApi SchoolsApi
        {
            get
            {
                return new EdFi.Roster.Sdk.Api.EnrollmentComposites.SchoolsApi(this.url);
            }
        }

        public EdFi.Roster.Sdk.Api.EnrollmentComposites.IStaffsApi StaffsApi
        {
            get
            {
                return new EdFi.Roster.Sdk.Api.EnrollmentComposites.StaffsApi(this.url);
            }
        }

        public EdFi.Roster.Sdk.Api.EnrollmentComposites.IStudentsApi StudentsApi
        {
            get
            {
                return new EdFi.Roster.Sdk.Api.EnrollmentComposites.StudentsApi(this.url);
            }
        }

        public EdFi.Roster.Sdk.Api.EnrollmentComposites.ISectionsApi SectionsApi
        {
            get
            {
                return new EdFi.Roster.Sdk.Api.EnrollmentComposites.SectionsApi(this.url);
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
