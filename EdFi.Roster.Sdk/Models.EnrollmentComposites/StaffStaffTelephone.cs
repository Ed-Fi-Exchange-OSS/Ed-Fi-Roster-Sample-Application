/* 
 * Ed-Fi Operational Data Store API
 *
 * The Ed-Fi ODS / API enables applications to read and write education data stored in an Ed-Fi ODS through a secure REST interface.  ***  > *Note: Consumers of ODS / API information should sanitize all data for display and storage. The ODS / API provides reasonable safeguards against cross-site scripting attacks and other malicious content, but the platform does not and cannot guarantee that the data it contains is free of all potentially harmful content.*  *** 
 *
 * OpenAPI spec version: 3
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = EdFi.Roster.Sdk.Client.SwaggerDateConverter;

namespace EdFi.Roster.Sdk.Models.EnrollmentComposites
{
    /// <summary>
    /// StaffStaffTelephone
    /// </summary>
    [DataContract]
    public partial class StaffStaffTelephone :  IEquatable<StaffStaffTelephone>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaffStaffTelephone" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected StaffStaffTelephone() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="StaffStaffTelephone" /> class.
        /// </summary>
        /// <param name="telephoneNumberTypeDescriptor">The type of communication number listed for an individual or organization. (required).</param>
        /// <param name="orderOfPriority">The order of priority assigned to telephone numbers to define which number to attempt first, second, etc..</param>
        /// <param name="telephoneNumber">The telephone number including the area code, and extension, if applicable. (required).</param>
        public StaffStaffTelephone(string telephoneNumberTypeDescriptor = default(string), int? orderOfPriority = default(int?), string telephoneNumber = default(string))
        {
            // to ensure "telephoneNumberTypeDescriptor" is required (not null)
            if (telephoneNumberTypeDescriptor == null)
            {
                throw new InvalidDataException("telephoneNumberTypeDescriptor is a required property for StaffStaffTelephone and cannot be null");
            }
            else
            {
                this.TelephoneNumberTypeDescriptor = telephoneNumberTypeDescriptor;
            }
            // to ensure "telephoneNumber" is required (not null)
            if (telephoneNumber == null)
            {
                throw new InvalidDataException("telephoneNumber is a required property for StaffStaffTelephone and cannot be null");
            }
            else
            {
                this.TelephoneNumber = telephoneNumber;
            }
            this.OrderOfPriority = orderOfPriority;
        }
        
        /// <summary>
        /// The type of communication number listed for an individual or organization.
        /// </summary>
        /// <value>The type of communication number listed for an individual or organization.</value>
        [DataMember(Name="telephoneNumberTypeDescriptor", EmitDefaultValue=false)]
        public string TelephoneNumberTypeDescriptor { get; set; }

        /// <summary>
        /// The order of priority assigned to telephone numbers to define which number to attempt first, second, etc.
        /// </summary>
        /// <value>The order of priority assigned to telephone numbers to define which number to attempt first, second, etc.</value>
        [DataMember(Name="orderOfPriority", EmitDefaultValue=false)]
        public int? OrderOfPriority { get; set; }

        /// <summary>
        /// The telephone number including the area code, and extension, if applicable.
        /// </summary>
        /// <value>The telephone number including the area code, and extension, if applicable.</value>
        [DataMember(Name="telephoneNumber", EmitDefaultValue=false)]
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class StaffStaffTelephone {\n");
            sb.Append("  TelephoneNumberTypeDescriptor: ").Append(TelephoneNumberTypeDescriptor).Append("\n");
            sb.Append("  OrderOfPriority: ").Append(OrderOfPriority).Append("\n");
            sb.Append("  TelephoneNumber: ").Append(TelephoneNumber).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as StaffStaffTelephone);
        }

        /// <summary>
        /// Returns true if StaffStaffTelephone instances are equal
        /// </summary>
        /// <param name="input">Instance of StaffStaffTelephone to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(StaffStaffTelephone input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.TelephoneNumberTypeDescriptor == input.TelephoneNumberTypeDescriptor ||
                    (this.TelephoneNumberTypeDescriptor != null &&
                    this.TelephoneNumberTypeDescriptor.Equals(input.TelephoneNumberTypeDescriptor))
                ) && 
                (
                    this.OrderOfPriority == input.OrderOfPriority ||
                    (this.OrderOfPriority != null &&
                    this.OrderOfPriority.Equals(input.OrderOfPriority))
                ) && 
                (
                    this.TelephoneNumber == input.TelephoneNumber ||
                    (this.TelephoneNumber != null &&
                    this.TelephoneNumber.Equals(input.TelephoneNumber))
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
                if (this.TelephoneNumberTypeDescriptor != null)
                    hashCode = hashCode * 59 + this.TelephoneNumberTypeDescriptor.GetHashCode();
                if (this.OrderOfPriority != null)
                    hashCode = hashCode * 59 + this.OrderOfPriority.GetHashCode();
                if (this.TelephoneNumber != null)
                    hashCode = hashCode * 59 + this.TelephoneNumber.GetHashCode();
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
            // TelephoneNumberTypeDescriptor (string) maxLength
            if(this.TelephoneNumberTypeDescriptor != null && this.TelephoneNumberTypeDescriptor.Length > 306)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for TelephoneNumberTypeDescriptor, length must be less than 306.", new [] { "TelephoneNumberTypeDescriptor" });
            }

            // TelephoneNumber (string) maxLength
            if(this.TelephoneNumber != null && this.TelephoneNumber.Length > 24)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for TelephoneNumber, length must be less than 24.", new [] { "TelephoneNumber" });
            }

            yield break;
        }
    }

}
