using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coffeefilter.Models
{
    public class ApplicationDbContext : System.Data.Entity.DbContext
    {
        public ApplicationDbContext() : base("name=DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<CrawlResult> CrawlResulsts { get; set; }
        public DbSet<CrawlSession> CrawlSessions { get; set; }
        public DbSet<CrawlEmail> CrawlEmails { get; set; }



    }


    public class CrawlSession
    {
        public CrawlSession()
        {
            CrawlResult = new List<CrawlResult>();
        }
        //primary key   
        public int CrawlSessionId { get; set; }
        //properties
        public long totalPagesCrawled { get; set; }
        public long CrawlDuration { get; set; }
        public bool completed { get; set; }
        public long bytesReceived { get; set; }
        public DateTime startDate { get; set; }

        //foreign key relationships
        public virtual ICollection<CrawlResult> CrawlResult { get; set; }


    }

    public class CrawlResult
    {
        public CrawlResult()
        {
            allLinkedUrlsInDomain = new List<string>();
            emails = new List<CrawlEmail>();
        }
        //primary Key
        public int CrawlResultId { get; set; }
        //properties
        public bool completed { get; set; }
        public string originatingWebsite { get; set; }
        public string BusinessName { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string phoneNumber { get; set; }
        public bool isHttps { get; set; }
        public string baseUrl { get; set; }
        public long bytesReceived { get; set; }
        public DateTime timeFinished { get; set; }

        //foreign key relationships
        public virtual CrawlSession CrawlSession { get; set; }
        public virtual ICollection<CrawlEmail> emails { get; set; }

        //untracked in db, only populated during crawl session.
        public List<string> allLinkedUrlsInDomain;


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

    public class CrawlEmail
    {
        public CrawlEmail()
        {

        }
        //primary key
        public int CrawlEmailId { get; set; }
        //properties
        public string sourceUrl { get; set; }
        public string emailAddress { get; set; }

        //foreign key relationship
        public virtual CrawlResult CrawlResult { get; set; }
    }
}
