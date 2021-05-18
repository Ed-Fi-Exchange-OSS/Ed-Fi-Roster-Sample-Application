using EdFi.Roster.Sdk.Client;

namespace EdFi.Roster.Services.ApiSdk
{
    public class ApiFacade
    {
        private readonly Configuration _configuration;

        public ApiFacade(Configuration configuration)
        {
            _configuration = configuration;
        }

        public Sdk.Api.EnrollmentComposites.ILocalEducationAgenciesApi LocalEducationAgenciesApi =>
            new Sdk.Api.EnrollmentComposites.LocalEducationAgenciesApi(_configuration);

        public Sdk.Api.EnrollmentComposites.ISchoolsApi SchoolsApi =>
            new Sdk.Api.EnrollmentComposites.SchoolsApi(_configuration);

        public Sdk.Api.EnrollmentComposites.IStaffsApi StaffsApi =>
            new Sdk.Api.EnrollmentComposites.StaffsApi(_configuration);

        public Sdk.Api.EnrollmentComposites.IStudentsApi StudentsApi =>
            new Sdk.Api.EnrollmentComposites.StudentsApi(_configuration);

        public Sdk.Api.EnrollmentComposites.ISectionsApi SectionsApi =>
            new Sdk.Api.EnrollmentComposites.SectionsApi(_configuration);
    }
}
