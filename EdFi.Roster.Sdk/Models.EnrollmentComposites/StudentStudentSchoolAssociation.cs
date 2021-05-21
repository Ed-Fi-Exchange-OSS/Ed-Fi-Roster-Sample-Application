/*
 * Ed-Fi Operational Data Store API
 *
 * The Ed-Fi ODS / API enables applications to read and write education data stored in an Ed-Fi ODS through a secure REST interface.  ***  > *Note: Consumers of ODS / API information should sanitize all data for display and storage. The ODS / API provides reasonable safeguards against cross-site scripting attacks and other malicious content, but the platform does not and cannot guarantee that the data it contains is free of all potentially harmful content.*  *** 
 *
 * The version of the OpenAPI document: 3
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using OpenAPIDateConverter = EdFi.Roster.Sdk.Client.OpenAPIDateConverter;

namespace EdFi.Roster.Sdk.Models.EnrollmentComposites
{
    /// <summary>
    /// StudentStudentSchoolAssociation
    /// </summary>
    [DataContract(Name = "student_studentSchoolAssociation")]
    public partial class StudentStudentSchoolAssociation : IEquatable<StudentStudentSchoolAssociation>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentStudentSchoolAssociation" /> class.
        /// </summary>
        [JsonConstructor]
        protected StudentStudentSchoolAssociation() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentStudentSchoolAssociation" /> class.
        /// </summary>
        /// <param name="entryGradeLevelDescriptor">The grade level or primary instructional level at which a student enters and receives services in a school or an educational institution during a given academic session. (required).</param>
        /// <param name="enrollmentBeginDate">The month, day, and year on which an individual enters and begins to receive instructional services in a school. (required).</param>
        /// <param name="enrollmentEndDate">The recorded exit or withdraw date for the student..</param>
        /// <param name="id">id (required).</param>
        /// <param name="schoolId">The identifier assigned to a school. (required).</param>
        /// <param name="nameOfInstitution">The full, legally accepted name of the institution. (required).</param>
        public StudentStudentSchoolAssociation(string entryGradeLevelDescriptor = default(string), DateTime enrollmentBeginDate = default(DateTime), DateTime enrollmentEndDate = default(DateTime), string id = default(string), int schoolId = default(int), string nameOfInstitution = default(string))
        {
            // to ensure "entryGradeLevelDescriptor" is required (not null)
            this.EntryGradeLevelDescriptor = entryGradeLevelDescriptor ?? throw new ArgumentNullException("entryGradeLevelDescriptor is a required property for StudentStudentSchoolAssociation and cannot be null");
            this.EnrollmentBeginDate = enrollmentBeginDate;
            // to ensure "id" is required (not null)
            this.Id = id ?? throw new ArgumentNullException("id is a required property for StudentStudentSchoolAssociation and cannot be null");
            this.SchoolId = schoolId;
            // to ensure "nameOfInstitution" is required (not null)
            this.NameOfInstitution = nameOfInstitution ?? throw new ArgumentNullException("nameOfInstitution is a required property for StudentStudentSchoolAssociation and cannot be null");
            this.EnrollmentEndDate = enrollmentEndDate;
        }

        /// <summary>
        /// The grade level or primary instructional level at which a student enters and receives services in a school or an educational institution during a given academic session.
        /// </summary>
        /// <value>The grade level or primary instructional level at which a student enters and receives services in a school or an educational institution during a given academic session.</value>
        [DataMember(Name = "entryGradeLevelDescriptor", IsRequired = true, EmitDefaultValue = false)]
        public string EntryGradeLevelDescriptor { get; set; }

        /// <summary>
        /// The month, day, and year on which an individual enters and begins to receive instructional services in a school.
        /// </summary>
        /// <value>The month, day, and year on which an individual enters and begins to receive instructional services in a school.</value>
        [DataMember(Name = "enrollmentBeginDate", IsRequired = true, EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime EnrollmentBeginDate { get; set; }

        /// <summary>
        /// The recorded exit or withdraw date for the student.
        /// </summary>
        /// <value>The recorded exit or withdraw date for the student.</value>
        [DataMember(Name = "enrollmentEndDate", EmitDefaultValue = false)]
        [JsonConverter(typeof(OpenAPIDateConverter))]
        public DateTime EnrollmentEndDate { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", IsRequired = true, EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The identifier assigned to a school.
        /// </summary>
        /// <value>The identifier assigned to a school.</value>
        [DataMember(Name = "schoolId", IsRequired = true, EmitDefaultValue = false)]
        public int SchoolId { get; set; }

        /// <summary>
        /// The full, legally accepted name of the institution.
        /// </summary>
        /// <value>The full, legally accepted name of the institution.</value>
        [DataMember(Name = "nameOfInstitution", IsRequired = true, EmitDefaultValue = false)]
        public string NameOfInstitution { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class StudentStudentSchoolAssociation {\n");
            sb.Append("  EntryGradeLevelDescriptor: ").Append(EntryGradeLevelDescriptor).Append("\n");
            sb.Append("  EnrollmentBeginDate: ").Append(EnrollmentBeginDate).Append("\n");
            sb.Append("  EnrollmentEndDate: ").Append(EnrollmentEndDate).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  SchoolId: ").Append(SchoolId).Append("\n");
            sb.Append("  NameOfInstitution: ").Append(NameOfInstitution).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as StudentStudentSchoolAssociation);
        }

        /// <summary>
        /// Returns true if StudentStudentSchoolAssociation instances are equal
        /// </summary>
        /// <param name="input">Instance of StudentStudentSchoolAssociation to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(StudentStudentSchoolAssociation input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.EntryGradeLevelDescriptor == input.EntryGradeLevelDescriptor ||
                    (this.EntryGradeLevelDescriptor != null &&
                    this.EntryGradeLevelDescriptor.Equals(input.EntryGradeLevelDescriptor))
                ) && 
                (
                    this.EnrollmentBeginDate == input.EnrollmentBeginDate ||
                    (this.EnrollmentBeginDate != null &&
                    this.EnrollmentBeginDate.Equals(input.EnrollmentBeginDate))
                ) && 
                (
                    this.EnrollmentEndDate == input.EnrollmentEndDate ||
                    (this.EnrollmentEndDate != null &&
                    this.EnrollmentEndDate.Equals(input.EnrollmentEndDate))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.SchoolId == input.SchoolId ||
                    this.SchoolId.Equals(input.SchoolId)
                ) && 
                (
                    this.NameOfInstitution == input.NameOfInstitution ||
                    (this.NameOfInstitution != null &&
                    this.NameOfInstitution.Equals(input.NameOfInstitution))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.EntryGradeLevelDescriptor != null)
                    hashCode = hashCode * 59 + this.EntryGradeLevelDescriptor.GetHashCode();
                if (this.EnrollmentBeginDate != null)
                    hashCode = hashCode * 59 + this.EnrollmentBeginDate.GetHashCode();
                if (this.EnrollmentEndDate != null)
                    hashCode = hashCode * 59 + this.EnrollmentEndDate.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                hashCode = hashCode * 59 + this.SchoolId.GetHashCode();
                if (this.NameOfInstitution != null)
                    hashCode = hashCode * 59 + this.NameOfInstitution.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            // EntryGradeLevelDescriptor (string) maxLength
            if(this.EntryGradeLevelDescriptor != null && this.EntryGradeLevelDescriptor.Length > 306)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for EntryGradeLevelDescriptor, length must be less than 306.", new [] { "EntryGradeLevelDescriptor" });
            }

            // NameOfInstitution (string) maxLength
            if(this.NameOfInstitution != null && this.NameOfInstitution.Length > 75)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for NameOfInstitution, length must be less than 75.", new [] { "NameOfInstitution" });
            }

            yield break;
        }
    }

}