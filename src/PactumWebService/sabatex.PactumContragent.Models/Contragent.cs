using System;
using System.Collections.Generic;
using System.Text;

namespace sabatex.PactumContragent.Models
{
    public class Contragent
    {
        public string FullNameEng { get; set; }
        public string DirectorFirstName { get; set; }
        public string DirectorMiddleName { get; set; }
        public string DirectorLastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FoundersList { get; set; }
        public string KVEDs { get; set; }
        public string OPFG { get; set; }

        public string StatutoryDocumentName { get; set; }// null
        public string StatutoryCapital { get; set; }
        public string Subdivisions { get; set; }
        public string FailureProccess { get; set; }
        public string StopProccess { get; set; }
        public string StopProccessInformation { get; set; }
        public string StopRecord { get; set; }
        public string CancelStopRecord { get; set; }
        public string AssigneeOtherOrganizations { get; set; }
        public string AssigneeThisOrganizations { get; set; }
        public string DeadlineForCreditors { get; set; }
        public string DateAndNumberRecordInEdrBeforeLaw { get; set; }
        public string DateAndNumberRecordInEdrAfterLaw { get; set; }
        public string DateAndNumberRecordInEdrTransformation { get; set; }
        public string DateOfEnforcementProceedings { get; set; }
        public string RegulatoryAuthorities { get; set; }
        public string MainKVED { get; set; }
        public string ChangeLocationTerm { get; set; }
        public string AdministrativeAct { get; set; }
        public string ESVinformation { get; set; }
        public string EsvProfessionalRisk { get; set; }
        public string LocationRegistrationFile { get; set; }
        public string DirectorInformation { get; set; }
        public string IndividualTaxNumber { get; set; }
        public string VatRegistrationDate { get; set; }
        public string VatCancellationDate { get; set; }
        public string CancellationReason { get; set; }
        public string VatSpecialTaxStartDate { get; set; }
        public string VatFromSpecialToOverallDate { get; set; }
        public string IndividualTaxNumberOld { get; set; }
        public string VatCertificate { get; set; }
        public string TaxSystem { get; set; }
        public string SingleTaxStartDate { get; set; }
        public string SingleTaxRate { get; set; }
        public string SingleTaxGroup { get; set; }
        public string SingleTaxEndDate { get; set; }
        public string Courts { get; set; }
        public RegisterStates[] RegisterStates { get; set; }
        public string Status { get; set; }
        public string CompanyStartDate { get; set; }
        public string TaxAdministrationStatus { get; set; }
        public string TaxDebt { get; set; }
        public string CompanyStatus { get; set; }
        public ContractorAuditPlans[] ContractorAuditPlans { get; set; }
        public bool IsFilia { get; set; }
        public string Address_atu { get; set; }
        public string Address_atu_code { get; set; }
        public string Address_street { get; set; }
        public string Address_house_type { get; set; }
        public string Address_house { get; set; }
        public string Address_building_type { get; set; }
        public string Address_building { get; set; }
        public string Address_num_type { get; set; }
        public string Address_num { get; set; }
        public string Address_zip { get; set; }
        public string Address_country { get; set; }
        public bool IsAddressElements { get; set; }
        public string[] Contacts_phones { get; set; }
        public string Contacts_email { get; set; }
        public string Contacts_fax { get; set; }
        public string ShortOpfgName { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
        public string Address { get; set; }
        public bool? IsFop { get; set; }
    }
}
