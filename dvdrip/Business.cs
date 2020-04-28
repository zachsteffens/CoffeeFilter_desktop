using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;


namespace coffeefilter
{
    [DelimitedRecord(",")]
    public class outputBusiness
    {
        
        public string BusinessName { get; set; }
        public string originatingWebsite { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string phoneNumber { get; set; }
        public string emails { get; set; }
        public string dateCrawled { get; set; }

        public string County { get; set; }
        public string MetroArea { get; set; }
        public string Neighborhood { get; set; }
        public string CompanyDescription { get; set; }
        public string PrimarySICCode { get; set; }
        public string PrimarySICDescription { get; set; }
        public string PrimarySICAdSize { get; set; }
        public string PrimarySICYearAppeared { get; set; }
        public string SICCode1 { get; set; }
        public string SICCode1Description { get; set; }
        public string SICCode2 { get; set; }
        public string SICCode2Description { get; set; }
        public string SICCode3 { get; set; }
        public string SICCode3Description { get; set; }
        public string PrimaryNAICS { get; set; }
        public string PrimaryNAICSDescription { get; set; }
        public string NAICS1 { get; set; }
        public string NAICS1Description { get; set; }
        public string NAICS2 { get; set; }
        public string NAICS2Description { get; set; }
        public string NAICS3 { get; set; }
        public string NAICS3Description { get; set; }
        public string FranchiseDescription1 { get; set; }
        public string FranchiseDescription2 { get; set; }
        public string LocationEmployeeSizeRange { get; set; }
        public string LocationEmployeeSizeActual { get; set; }
        public string LocationSalesVolumeRange { get; set; }
        public string LocationSalesVolumeActual { get; set; }
        public string CorporateEmployeeSizeRange { get; set; }
        public string CorporateEmployeeSizeActual { get; set; }
        public string CorporateSalesVolumeRange { get; set; }
        public string CorporateSalesVolumeActual { get; set; }
        public string TypeofBusiness { get; set; }
        public string LocationType { get; set; }
        public string YearsInDatabase { get; set; }
        public string YearEstablished { get; set; }
        public string SquareFootage { get; set; }
        public string HomeBusiness { get; set; }
        public string CreditScoreAlpha { get; set; }
        public string ExecutiveFirstName1 { get; set; }
        public string ExecutiveLastName1 { get; set; }
        public string ExecutiveTitle1 { get; set; }
        public string ExecutiveGender1 { get; set; }
        public string ExecutiveFirstName2 { get; set; }
        public string ExecutiveLastName2 { get; set; }
        public string ExecutiveTitle2 { get; set; }
        public string ExecutiveGender2 { get; set; }
        public string ExecutiveFirstName3 { get; set; }
        public string ExecutiveLastName3 { get; set; }
        public string ExecutiveTitle3 { get; set; }
        public string ExecutiveGender3 { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }
        public string Facebook { get; set; }

    }

    [DelimitedRecord(",")]
    [IgnoreFirst]
    public class Business
    {
        public string CompanyName;
        public string ExecutiveFirstName;
        public string ExecutiveLastName;
        public string ProfessionalTitle;
        public string ExecutiveTitle;
        public string ExecutiveGender;
        public string Address;
        public string City;
        public string State;
        public string ZIPCode;
        public string ZIPFour;
        public string CarrierRoute;
        public string DeliveryPointBarcode;
        public string County;
        public string MetroArea;
        public string Neighborhood;
        public string PhoneNumberCombined;
        public string FaxNumberCombined;
        public string TollFreeNumberCombined;
        public string Website;
        public string CompanyDescription;
        public string PrimarySICCode;
        public string PrimarySICDescription;
        public string PrimarySICAdSize;
        public string PrimarySICYearAppeared;
        public string SICCode1;
        public string SICCode1Description;
        public string SICCode1AdSize;
        public string SICCode1YearAppeared;
        public string SICCode2;
        public string SICCode2Description;
        public string SICCode2AdSize;
        public string SICCode2YearAppeared;
        public string SICCode3;
        public string SICCode3Description;
        public string SICCode3AdSize;
        public string SICCode3YearAppeared;
        public string SICCode4;
        public string SICCode4Description;
        public string SICCode4AdSize;
        public string SICCode4YearAppeared;
        public string SICCode5;
        public string SICCode5Description;
        public string SICCode5AdSize;
        public string SICCode5YearAppeared;
        public string SICCode6;
        public string SICCode6Description;
        public string SICCode6AdSize;
        public string SICCode6YearAppeared;
        public string SICCode7;
        public string SICCode7Description;
        public string SICCode7AdSize;
        public string SICCode7YearAppeared;
        public string SICCode8;
        public string SICCode8Description;
        public string SICCode8AdSize;
        public string SICCode8YearAppeared;
        public string SICCode9;
        public string SICCode9Description;
        public string SICCode9AdSize;
        public string SICCode9YearAppeared;
        public string SICCode10;
        public string SICCode10Description;
        public string SICCode10AdSize;
        public string SICCode10YearAppeared;
        public string PrimaryNAICS;
        public string PrimaryNAICSDescription;
        public string NAICS1;
        public string NAICS1Description;
        public string NAICS2;
        public string NAICS2Description;
        public string NAICS3;
        public string NAICS3Description;
        public string NAICS4;
        public string NAICS4Description;
        public string NAICS5;
        public string NAICS5Description;
        public string NAICS6;
        public string NAICS6Description;
        public string NAICS7;
        public string NAICS7Description;
        public string NAICS8;
        public string NAICS8Description;
        public string NAICS9;
        public string NAICS9Description;
        public string NAICS10;
        public string NAICS10Description;
        public string FranchiseDescription1;
        public string FranchiseDescription2;
        public string FranchiseDescription3;
        public string FranchiseDescription4;
        public string FranchiseDescription5;
        public string CuisineCode;
        public string CuisineCodeDescription;
        public string LocationEmployeeSizeRange;
        public string LocationEmployeeSizeActual;
        public string LocationSalesVolumeRange;
        public string LocationSalesVolumeActual;
        public string CorporateEmployeeSizeRange;
        public string CorporateEmployeeSizeActual;
        public string CorporateSalesVolumeRange;
        public string CorporateSalesVolumeActual;
        public string TypeofBusiness;
        public string LocationType;
        public string IUSANumber;
        public string ParentIUSANumber;
        public string SubsidiaryIUSANumber;
        public string ForeignParentFlag;
        public string EIN1;
        public string EIN2;
        public string EIN3;
        public string Fortune1000Ranking;
        public string CreditCardsAccepted;
        public string LastUpdatedOn;
        public string YearsInDatabase;
        public string YearEstablished;
        public string SquareFootage;
        public string HomeBusiness;
        public string CreditScoreAlpha;
        public string GovernmentOffice;
        public string ImportExportFlag;
        public string OwnorLease;
        public string FirmorIndividual;
        public string MondayOpen;
        public string MondayClose;
        public string TuesdayOpen;
        public string TuesdayClose;
        public string WednesdayOpen;
        public string WednesdayClose;
        public string ThursdayOpen;
        public string ThursdayClose;
        public string FridayOpen;
        public string FridayClose;
        public string SaturdayOpen;
        public string SaturdayClose;
        public string SundayOpen;
        public string SundayClose;
        public string ExecutiveFirstName1;
        public string ExecutiveLastName1;
        public string ExecutiveTitle1;
        public string ExecutiveGender1;
        public string ExecutiveFirstName2;
        public string ExecutiveLastName2;
        public string ExecutiveTitle2;
        public string ExecutiveGender2;
        public string ExecutiveFirstName3;
        public string ExecutiveLastName3;
        public string ExecutiveTitle3;
        public string ExecutiveGender3;
        public string ExecutiveFirstName4;
        public string ExecutiveLastName4;
        public string ExecutiveTitle4;
        public string ExecutiveGender4;
        public string ExecutiveFirstName5;
        public string ExecutiveLastName5;
        public string ExecutiveTitle5;
        public string ExecutiveGender5;
        public string ExecutiveFirstName6;
        public string ExecutiveLastName6;
        public string ExecutiveTitle6;
        public string ExecutiveGender6;
        public string ExecutiveFirstName7;
        public string ExecutiveLastName7;
        public string ExecutiveTitle7;
        public string ExecutiveGender7;
        public string ExecutiveFirstName8;
        public string ExecutiveLastName8;
        public string ExecutiveTitle8;
        public string ExecutiveGender8;
        public string ExecutiveFirstName9;
        public string ExecutiveLastName9;
        public string ExecutiveTitle9;
        public string ExecutiveGender9;
        public string ExecutiveFirstName10;
        public string ExecutiveLastName10;
        public string ExecutiveTitle10;
        public string ExecutiveGender10;
        public string ExecutiveFirstName11;
        public string ExecutiveLastName11;
        public string ExecutiveTitle11;
        public string ExecutiveGender11;
        public string ExecutiveFirstName12;
        public string ExecutiveLastName12;
        public string ExecutiveTitle12;
        public string ExecutiveGender12;
        public string ExecutiveFirstName13;
        public string ExecutiveLastName13;
        public string ExecutiveTitle13;
        public string ExecutiveGender13;
        public string ExecutiveFirstName14;
        public string ExecutiveLastName14;
        public string ExecutiveTitle14;
        public string ExecutiveGender14;
        public string ExecutiveFirstName15;
        public string ExecutiveLastName15;
        public string ExecutiveTitle15;
        public string ExecutiveGender15;
        public string ExecutiveFirstName16;
        public string ExecutiveLastName16;
        public string ExecutiveTitle16;
        public string ExecutiveGender16;
        public string ExecutiveFirstName17;
        public string ExecutiveLastName17;
        public string ExecutiveTitle17;
        public string ExecutiveGender17;
        public string ExecutiveFirstName18;
        public string ExecutiveLastName18;
        public string ExecutiveTitle18;
        public string ExecutiveGender18;
        public string ExecutiveFirstName19;
        public string ExecutiveLastName19;
        public string ExecutiveTitle19;
        public string ExecutiveGender19;
        public string ExecutiveFirstName20;
        public string ExecutiveLastName20;
        public string ExecutiveTitle20;
        public string ExecutiveGender20;
        public string ExecutiveFirstName21;
        public string ExecutiveLastName21;
        public string ExecutiveTitle21;
        public string ExecutiveGender21;
        public string ExecutiveFirstName22;
        public string ExecutiveLastName22;
        public string ExecutiveTitle22;
        public string ExecutiveGender22;
        public string ExecutiveFirstName23;
        public string ExecutiveLastName23;
        public string ExecutiveTitle23;
        public string ExecutiveGender23;
        public string ExecutiveFirstName24;
        public string ExecutiveLastName24;
        public string ExecutiveTitle24;
        public string ExecutiveGender24;
        public string ExecutiveFirstName25;
        public string ExecutiveLastName25;
        public string ExecutiveTitle25;
        public string ExecutiveGender25;
        public string ExecutiveFirstName26;
        public string ExecutiveLastName26;
        public string ExecutiveTitle26;
        public string ExecutiveGender26;
        public string ExecutiveFirstName27;
        public string ExecutiveLastName27;
        public string ExecutiveTitle27;
        public string ExecutiveGender27;
        public string ExecutiveFirstName28;
        public string ExecutiveLastName28;
        public string ExecutiveTitle28;
        public string ExecutiveGender28;
        public string ExecutiveFirstName29;
        public string ExecutiveLastName29;
        public string ExecutiveTitle29;
        public string ExecutiveGender29;
        public string ExecutiveFirstName30;
        public string ExecutiveLastName30;
        public string ExecutiveTitle30;
        public string ExecutiveGender30;
        public string ExecutiveFirstName31;
        public string ExecutiveLastName31;
        public string ExecutiveTitle31;
        public string ExecutiveGender31;
        public string ExecutiveFirstName32;
        public string ExecutiveLastName32;
        public string ExecutiveTitle32;
        public string ExecutiveGender32;
        public string ExecutiveFirstName33;
        public string ExecutiveLastName33;
        public string ExecutiveTitle33;
        public string ExecutiveGender33;
        public string ExecutiveFirstName34;
        public string ExecutiveLastName34;
        public string ExecutiveTitle34;
        public string ExecutiveGender34;
        public string ExecutiveFirstName35;
        public string ExecutiveLastName35;
        public string ExecutiveTitle35;
        public string ExecutiveGender35;
        public string ExecutiveFirstName36;
        public string ExecutiveLastName36;
        public string ExecutiveTitle36;
        public string ExecutiveGender36;
        public string ExecutiveFirstName37;
        public string ExecutiveLastName37;
        public string ExecutiveTitle37;
        public string ExecutiveGender37;
        public string ExecutiveFirstName38;
        public string ExecutiveLastName38;
        public string ExecutiveTitle38;
        public string ExecutiveGender38;
        public string ExecutiveFirstName39;
        public string ExecutiveLastName39;
        public string ExecutiveTitle39;
        public string ExecutiveGender39;
        public string ExecutiveFirstName40;
        public string ExecutiveLastName40;
        public string ExecutiveTitle40;
        public string ExecutiveGender40;
        public string ExecutiveFirstName41;
        public string ExecutiveLastName41;
        public string ExecutiveTitle41;
        public string ExecutiveGender41;
        public string ExecutiveFirstName42;
        public string ExecutiveLastName42;
        public string ExecutiveTitle42;
        public string ExecutiveGender42;
        public string ExecutiveFirstName43;
        public string ExecutiveLastName43;
        public string ExecutiveTitle43;
        public string ExecutiveGender43;
        public string ExecutiveFirstName44;
        public string ExecutiveLastName44;
        public string ExecutiveTitle44;
        public string ExecutiveGender44;
        public string ExecutiveFirstName45;
        public string ExecutiveLastName45;
        public string ExecutiveTitle45;
        public string ExecutiveGender45;
        public string ExecutiveFirstName46;
        public string ExecutiveLastName46;
        public string ExecutiveTitle46;
        public string ExecutiveGender46;
        public string ExecutiveFirstName47;
        public string ExecutiveLastName47;
        public string ExecutiveTitle47;
        public string ExecutiveGender47;
        public string ExecutiveFirstName48;
        public string ExecutiveLastName48;
        public string ExecutiveTitle48;
        public string ExecutiveGender48;
        public string ExecutiveFirstName49;
        public string ExecutiveLastName49;
        public string ExecutiveTitle49;
        public string ExecutiveGender49;
        public string ExecutiveFirstName50;
        public string ExecutiveLastName50;
        public string ExecutiveTitle50;
        public string ExecutiveGender50;
        public string TickerSymbol;
        public string StockExchange;
        public string AccountingExpenses;
        public string AdvertisingExpenses;
        public string ComputerExpenses;
        public string ContractLaborExpenses;
        public string InsuranceExpenses;
        public string LegalExpenses;
        public string OfficeSuppliesExpense;
        public string ManagementAdministrationExpenses;
        public string PackageContainerExpense;
        public string PayrollandBenefitsExpenses;
        public string PurchasePrintExpenses;
        public string RentExpenses;
        public string TelcomExpenses;
        public string UtilitiesExpenses;
        public string MailingAddress;
        public string MailingCity;
        public string MailingState;
        public string MailingZipCode;
        public string MailingZipFour;
        public string MailingCarrierRoute;
        public string MailingDeliveryPointBarCode;
        public string Twitter;
        public string LinkedIn;
        public string Facebook;
        public string FranchiseSpecialtyCode20;
        public string FranchiseSpecialtyCode21;
        public string FranchiseSpecialtyCode30;
        public string FranchiseSpecialtyCode31;
        public string FranchiseSpecialtyCode40;
        public string FranchiseSpecialtyCode41;
        public string FranchiseSpecialtyCode50;
        public string FranchiseSpecialtyCode51;
        public string FranchiseSpecialtyCode60;
        public string AffiliatedRecords;
        public string FranchiseSpecialtyCode61;
        public string AffiliatedLocations;
        public string FederalContractor;
        public string CensusBlockGroup;
        public string RecordType;
    }
}
