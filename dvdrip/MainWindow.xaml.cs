using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management;
using System.Net;
using Newtonsoft;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Text.RegularExpressions;
using FileHelpers;
using System.Data;
using coffeefilter.Models;
using System.Net.Http;
using System.Reflection;
using Abot2.Poco;
using Abot2.Crawler;
using Abot2.Util;

namespace coffeefilter
{
    public partial class MainWindow : Window
    {
        private string csvText { get; set; }
        public const int NUMBER_OF_URLS_TO_CRAWL = 40;
        public const string MIN_TIME = "01/01/1754";
        public static string betterEmailRegex = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
        public static string emailRegex = @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}\b";
        public static string cleanQuotedStrings = @"""(.*?)""";
        public static string urlRegex = @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";
        public static string relativeUrlRegex = @"href=""\/{1}[^\/]\S+""";
        public static string[] exdludedEmailExtensions = { "doc", "png", "jpeg", "jpg", "pdf", "zip", "xls", "xslx", "gif", "bmp", "css", "js" };
        public static string[] urlsToIgnore = { "wp-content", "blog", "wp-json", "plugin", "themes", ".css", ".js", ".min" };
        public static string[] allTLDs = { "AAA", "AARP", "ABARTH", "ABB", "ABBOTT", "ABBVIE", "ABC", "ABLE", "ABOGADO", "ABUDHABI", "AC", "ACADEMY", "ACCENTURE", "ACCOUNTANT", "ACCOUNTANTS", "ACO", "ACTOR", "AD", "ADAC", "ADS", "ADULT", "AE", "AEG", "AERO", "AETNA", "AF", "AFAMILYCOMPANY", "AFL", "AFRICA", "AG", "AGAKHAN", "AGENCY", "AI", "AIG", "AIGO", "AIRBUS", "AIRFORCE", "AIRTEL", "AKDN", "AL", "ALFAROMEO", "ALIBABA", "ALIPAY", "ALLFINANZ", "ALLSTATE", "ALLY", "ALSACE", "ALSTOM", "AM", "AMERICANEXPRESS", "AMERICANFAMILY", "AMEX", "AMFAM", "AMICA", "AMSTERDAM", "ANALYTICS", "ANDROID", "ANQUAN", "ANZ", "AO", "AOL", "APARTMENTS", "APP", "APPLE", "AQ", "AQUARELLE", "AR", "ARAB", "ARAMCO", "ARCHI", "ARMY", "ARPA", "ART", "ARTE", "AS", "ASDA", "ASIA", "ASSOCIATES", "AT", "ATHLETA", "ATTORNEY", "AU", "AUCTION", "AUDI", "AUDIBLE", "AUDIO", "AUSPOST", "AUTHOR", "AUTO", "AUTOS", "AVIANCA", "AW", "AWS", "AX", "AXA", "AZ", "AZURE", "BA", "BABY", "BAIDU", "BANAMEX", "BANANAREPUBLIC", "BAND", "BANK", "BAR", "BARCELONA", "BARCLAYCARD", "BARCLAYS", "BAREFOOT", "BARGAINS", "BASEBALL", "BASKETBALL", "BAUHAUS", "BAYERN", "BB", "BBC", "BBT", "BBVA", "BCG", "BCN", "BD", "BE", "BEATS", "BEAUTY", "BEER", "BENTLEY", "BERLIN", "BEST", "BESTBUY", "BET", "BF", "BG", "BH", "BHARTI", "BI", "BIBLE", "BID", "BIKE", "BING", "BINGO", "BIO", "BIZ", "BJ", "BLACK", "BLACKFRIDAY", "BLOCKBUSTER", "BLOG", "BLOOMBERG", "BLUE", "BM", "BMS", "BMW", "BN", "BNPPARIBAS", "BO", "BOATS", "BOEHRINGER", "BOFA", "BOM", "BOND", "BOO", "BOOK", "BOOKING", "BOSCH", "BOSTIK", "BOSTON", "BOT", "BOUTIQUE", "BOX", "BR", "BRADESCO", "BRIDGESTONE", "BROADWAY", "BROKER", "BROTHER", "BRUSSELS", "BS", "BT", "BUDAPEST", "BUGATTI", "BUILD", "BUILDERS", "BUSINESS", "BUY", "BUZZ", "BV", "BW", "BY", "BZ", "BZH", "CA", "CAB", "CAFE", "CAL", "CALL", "CALVINKLEIN", "CAM", "CAMERA", "CAMP", "CANCERRESEARCH", "CANON", "CAPETOWN", "CAPITAL", "CAPITALONE", "CAR", "CARAVAN", "CARDS", "CARE", "CAREER", "CAREERS", "CARS", "CASA", "CASE", "CASEIH", "CASH", "CASINO", "CAT", "CATERING", "CATHOLIC", "CBA", "CBN", "CBRE", "CBS", "CC", "CD", "CEB", "CENTER", "CEO", "CERN", "CF", "CFA", "CFD", "CG", "CH", "CHANEL", "CHANNEL", "CHARITY", "CHASE", "CHAT", "CHEAP", "CHINTAI", "CHRISTMAS", "CHROME", "CHURCH", "CI", "CIPRIANI", "CIRCLE", "CISCO", "CITADEL", "CITI", "CITIC", "CITY", "CITYEATS", "CK", "CL", "CLAIMS", "CLEANING", "CLICK", "CLINIC", "CLINIQUE", "CLOTHING", "CLOUD", "CLUB", "CLUBMED", "CM", "CN", "CO", "COACH", "CODES", "COFFEE", "COLLEGE", "COLOGNE", "COM", "COMCAST", "COMMBANK", "COMMUNITY", "COMPANY", "COMPARE", "COMPUTER", "COMSEC", "CONDOS", "CONSTRUCTION", "CONSULTING", "CONTACT", "CONTRACTORS", "COOKING", "COOKINGCHANNEL", "COOL", "COOP", "CORSICA", "COUNTRY", "COUPON", "COUPONS", "COURSES", "CPA", "CR", "CREDIT", "CREDITCARD", "CREDITUNION", "CRICKET", "CROWN", "CRS", "CRUISE", "CRUISES", "CSC", "CU", "CUISINELLA", "CV", "CW", "CX", "CY", "CYMRU", "CYOU", "CZ", "DABUR", "DAD", "DANCE", "DATA", "DATE", "DATING", "DATSUN", "DAY", "DCLK", "DDS", "DE", "DEAL", "DEALER", "DEALS", "DEGREE", "DELIVERY", "DELL", "DELOITTE", "DELTA", "DEMOCRAT", "DENTAL", "DENTIST", "DESI", "DESIGN", "DEV", "DHL", "DIAMONDS", "DIET", "DIGITAL", "DIRECT", "DIRECTORY", "DISCOUNT", "DISCOVER", "DISH", "DIY", "DJ", "DK", "DM", "DNP", "DO", "DOCS", "DOCTOR", "DOG", "DOMAINS", "DOT", "DOWNLOAD", "DRIVE", "DTV", "DUBAI", "DUCK", "DUNLOP", "DUPONT", "DURBAN", "DVAG", "DVR", "DZ", "EARTH", "EAT", "EC", "ECO", "EDEKA", "EDU", "EDUCATION", "EE", "EG", "EMAIL", "EMERCK", "ENERGY", "ENGINEER", "ENGINEERING", "ENTERPRISES", "EPSON", "EQUIPMENT", "ER", "ERICSSON", "ERNI", "ES", "ESQ", "ESTATE", "ESURANCE", "ET", "ETISALAT", "EU", "EUROVISION", "EUS", "EVENTS", "EXCHANGE", "EXPERT", "EXPOSED", "EXPRESS", "EXTRASPACE", "FAGE", "FAIL", "FAIRWINDS", "FAITH", "FAMILY", "FAN", "FANS", "FARM", "FARMERS", "FASHION", "FAST", "FEDEX", "FEEDBACK", "FERRARI", "FERRERO", "FI", "FIAT", "FIDELITY", "FIDO", "FILM", "FINAL", "FINANCE", "FINANCIAL", "FIRE", "FIRESTONE", "FIRMDALE", "FISH", "FISHING", "FIT", "FITNESS", "FJ", "FK", "FLICKR", "FLIGHTS", "FLIR", "FLORIST", "FLOWERS", "FLY", "FM", "FO", "FOO", "FOOD", "FOODNETWORK", "FOOTBALL", "FORD", "FOREX", "FORSALE", "FORUM", "FOUNDATION", "FOX", "FR", "FREE", "FRESENIUS", "FRL", "FROGANS", "FRONTDOOR", "FRONTIER", "FTR", "FUJITSU", "FUJIXEROX", "FUN", "FUND", "FURNITURE", "FUTBOL", "FYI", "GA", "GAL", "GALLERY", "GALLO", "GALLUP", "GAME", "GAMES", "GAP", "GARDEN", "GAY", "GB", "GBIZ", "GD", "GDN", "GE", "GEA", "GENT", "GENTING", "GEORGE", "GF", "GG", "GGEE", "GH", "GI", "GIFT", "GIFTS", "GIVES", "GIVING", "GL", "GLADE", "GLASS", "GLE", "GLOBAL", "GLOBO", "GM", "GMAIL", "GMBH", "GMO", "GMX", "GN", "GODADDY", "GOLD", "GOLDPOINT", "GOLF", "GOO", "GOODYEAR", "GOOG", "GOOGLE", "GOP", "GOT", "GOV", "GP", "GQ", "GR", "GRAINGER", "GRAPHICS", "GRATIS", "GREEN", "GRIPE", "GROCERY", "GROUP", "GS", "GT", "GU", "GUARDIAN", "GUCCI", "GUGE", "GUIDE", "GUITARS", "GURU", "GW", "GY", "HAIR", "HAMBURG", "HANGOUT", "HAUS", "HBO", "HDFC", "HDFCBANK", "HEALTH", "HEALTHCARE", "HELP", "HELSINKI", "HERE", "HERMES", "HGTV", "HIPHOP", "HISAMITSU", "HITACHI", "HIV", "HK", "HKT", "HM", "HN", "HOCKEY", "HOLDINGS", "HOLIDAY", "HOMEDEPOT", "HOMEGOODS", "HOMES", "HOMESENSE", "HONDA", "HORSE", "HOSPITAL", "HOST", "HOSTING", "HOT", "HOTELES", "HOTELS", "HOTMAIL", "HOUSE", "HOW", "HR", "HSBC", "HT", "HU", "HUGHES", "HYATT", "HYUNDAI", "IBM", "ICBC", "ICE", "ICU", "ID", "IE", "IEEE", "IFM", "IKANO", "IL", "IM", "IMAMAT", "IMDB", "IMMO", "IMMOBILIEN", "IN", "INC", "INDUSTRIES", "INFINITI", "INFO", "ING", "INK", "INSTITUTE", "INSURANCE", "INSURE", "INT", "INTEL", "INTERNATIONAL", "INTUIT", "INVESTMENTS", "IO", "IPIRANGA", "IQ", "IR", "IRISH", "IS", "ISMAILI", "IST", "ISTANBUL", "IT", "ITAU", "ITV", "IVECO", "JAGUAR", "JAVA", "JCB", "JCP", "JE", "JEEP", "JETZT", "JEWELRY", "JIO", "JLL", "JM", "JMP", "JNJ", "JO", "JOBS", "JOBURG", "JOT", "JOY", "JP", "JPMORGAN", "JPRS", "JUEGOS", "JUNIPER", "KAUFEN", "KDDI", "KE", "KERRYHOTELS", "KERRYLOGISTICS", "KERRYPROPERTIES", "KFH", "KG", "KH", "KI", "KIA", "KIM", "KINDER", "KINDLE", "KITCHEN", "KIWI", "KM", "KN", "KOELN", "KOMATSU", "KOSHER", "KP", "KPMG", "KPN", "KR", "KRD", "KRED", "KUOKGROUP", "KW", "KY", "KYOTO", "KZ", "LA", "LACAIXA", "LAMBORGHINI", "LAMER", "LANCASTER", "LANCIA", "LAND", "LANDROVER", "LANXESS", "LASALLE", "LAT", "LATINO", "LATROBE", "LAW", "LAWYER", "LB", "LC", "LDS", "LEASE", "LECLERC", "LEFRAK", "LEGAL", "LEGO", "LEXUS", "LGBT", "LI", "LIDL", "LIFE", "LIFEINSURANCE", "LIFESTYLE", "LIGHTING", "LIKE", "LILLY", "LIMITED", "LIMO", "LINCOLN", "LINDE", "LINK", "LIPSY", "LIVE", "LIVING", "LIXIL", "LK", "LLC", "LLP", "LOAN", "LOANS", "LOCKER", "LOCUS", "LOFT", "LOL", "LONDON", "LOTTE", "LOTTO", "LOVE", "LPL", "LPLFINANCIAL", "LR", "LS", "LT", "LTD", "LTDA", "LU", "LUNDBECK", "LUPIN", "LUXE", "LUXURY", "LV", "LY", "MA", "MACYS", "MADRID", "MAIF", "MAISON", "MAKEUP", "MAN", "MANAGEMENT", "MANGO", "MAP", "MARKET", "MARKETING", "MARKETS", "MARRIOTT", "MARSHALLS", "MASERATI", "MATTEL", "MBA", "MC", "MCKINSEY", "MD", "ME", "MED", "MEDIA", "MEET", "MELBOURNE", "MEME", "MEMORIAL", "MEN", "MENU", "MERCKMSD", "METLIFE", "MG", "MH", "MIAMI", "MICROSOFT", "MIL", "MINI", "MINT", "MIT", "MITSUBISHI", "MK", "ML", "MLB", "MLS", "MM", "MMA", "MN", "MO", "MOBI", "MOBILE", "MODA", "MOE", "MOI", "MOM", "MONASH", "MONEY", "MONSTER", "MORMON", "MORTGAGE", "MOSCOW", "MOTO", "MOTORCYCLES", "MOV", "MOVIE", "MP", "MQ", "MR", "MS", "MSD", "MT", "MTN", "MTR", "MU", "MUSEUM", "MUTUAL", "MV", "MW", "MX", "MY", "MZ", "NA", "NAB", "NAGOYA", "NAME", "NATIONWIDE", "NATURA", "NAVY", "NBA", "NC", "NE", "NEC", "NET", "NETBANK", "NETFLIX", "NETWORK", "NEUSTAR", "NEW", "NEWHOLLAND", "NEWS", "NEXT", "NEXTDIRECT", "NEXUS", "NF", "NFL", "NG", "NGO", "NHK", "NI", "NICO", "NIKE", "NIKON", "NINJA", "NISSAN", "NISSAY", "NL", "NO", "NOKIA", "NORTHWESTERNMUTUAL", "NORTON", "NOW", "NOWRUZ", "NOWTV", "NP", "NR", "NRA", "NRW", "NTT", "NU", "NYC", "NZ", "OBI", "OBSERVER", "OFF", "OFFICE", "OKINAWA", "OLAYAN", "OLAYANGROUP", "OLDNAVY", "OLLO", "OM", "OMEGA", "ONE", "ONG", "ONL", "ONLINE", "ONYOURSIDE", "OOO", "OPEN", "ORACLE", "ORANGE", "ORG", "ORGANIC", "ORIGINS", "OSAKA", "OTSUKA", "OTT", "OVH", "PA", "PAGE", "PANASONIC", "PARIS", "PARS", "PARTNERS", "PARTS", "PARTY", "PASSAGENS", "PAY", "PCCW", "PE", "PET", "PF", "PFIZER", "PG", "PH", "PHARMACY", "PHD", "PHILIPS", "PHONE", "PHOTO", "PHOTOGRAPHY", "PHOTOS", "PHYSIO", "PICS", "PICTET", "PICTURES", "PID", "PIN", "PING", "PINK", "PIONEER", "PIZZA", "PK", "PL", "PLACE", "PLAY", "PLAYSTATION", "PLUMBING", "PLUS", "PM", "PN", "PNC", "POHL", "POKER", "POLITIE", "PORN", "POST", "PR", "PRAMERICA", "PRAXI", "PRESS", "PRIME", "PRO", "PROD", "PRODUCTIONS", "PROF", "PROGRESSIVE", "PROMO", "PROPERTIES", "PROPERTY", "PROTECTION", "PRU", "PRUDENTIAL", "PS", "PT", "PUB", "PW", "PWC", "PY", "QA", "QPON", "QUEBEC", "QUEST", "QVC", "RACING", "RADIO", "RAID", "RE", "READ", "REALESTATE", "REALTOR", "REALTY", "RECIPES", "RED", "REDSTONE", "REDUMBRELLA", "REHAB", "REISE", "REISEN", "REIT", "RELIANCE", "REN", "RENT", "RENTALS", "REPAIR", "REPORT", "REPUBLICAN", "REST", "RESTAURANT", "REVIEW", "REVIEWS", "REXROTH", "RICH", "RICHARDLI", "RICOH", "RIGHTATHOME", "RIL", "RIO", "RIP", "RMIT", "RO", "ROCHER", "ROCKS", "RODEO", "ROGERS", "ROOM", "RS", "RSVP", "RU", "RUGBY", "RUHR", "RUN", "RW", "RWE", "RYUKYU", "SA", "SAARLAND", "SAFE", "SAFETY", "SAKURA", "SALE", "SALON", "SAMSCLUB", "SAMSUNG", "SANDVIK", "SANDVIKCOROMANT", "SANOFI", "SAP", "SARL", "SAS", "SAVE", "SAXO", "SB", "SBI", "SBS", "SC", "SCA", "SCB", "SCHAEFFLER", "SCHMIDT", "SCHOLARSHIPS", "SCHOOL", "SCHULE", "SCHWARZ", "SCIENCE", "SCJOHNSON", "SCOR", "SCOT", "SD", "SE", "SEARCH", "SEAT", "SECURE", "SECURITY", "SEEK", "SELECT", "SENER", "SERVICES", "SES", "SEVEN", "SEW", "SEX", "SEXY", "SFR", "SG", "SH", "SHANGRILA", "SHARP", "SHAW", "SHELL", "SHIA", "SHIKSHA", "SHOES", "SHOP", "SHOPPING", "SHOUJI", "SHOW", "SHOWTIME", "SHRIRAM", "SI", "SILK", "SINA", "SINGLES", "SITE", "SJ", "SK", "SKI", "SKIN", "SKY", "SKYPE", "SL", "SLING", "SM", "SMART", "SMILE", "SN", "SNCF", "SO", "SOCCER", "SOCIAL", "SOFTBANK", "SOFTWARE", "SOHU", "SOLAR", "SOLUTIONS", "SONG", "SONY", "SOY", "SPACE", "SPORT", "SPOT", "SPREADBETTING", "SR", "SRL", "SS", "ST", "STADA", "STAPLES", "STAR", "STATEBANK", "STATEFARM", "STC", "STCGROUP", "STOCKHOLM", "STORAGE", "STORE", "STREAM", "STUDIO", "STUDY", "STYLE", "SU", "SUCKS", "SUPPLIES", "SUPPLY", "SUPPORT", "SURF", "SURGERY", "SUZUKI", "SV", "SWATCH", "SWIFTCOVER", "SWISS", "SX", "SY", "SYDNEY", "SYMANTEC", "SYSTEMS", "SZ", "TAB", "TAIPEI", "TALK", "TAOBAO", "TARGET", "TATAMOTORS", "TATAR", "TATTOO", "TAX", "TAXI", "TC", "TCI", "TD", "TDK", "TEAM", "TECH", "TECHNOLOGY", "TEL", "TEMASEK", "TENNIS", "TEVA", "TF", "TG", "TH", "THD", "THEATER", "THEATRE", "TIAA", "TICKETS", "TIENDA", "TIFFANY", "TIPS", "TIRES", "TIROL", "TJ", "TJMAXX", "TJX", "TK", "TKMAXX", "TL", "TM", "TMALL", "TN", "TO", "TODAY", "TOKYO", "TOOLS", "TOP", "TORAY", "TOSHIBA", "TOTAL", "TOURS", "TOWN", "TOYOTA", "TOYS", "TR", "TRADE", "TRADING", "TRAINING", "TRAVEL", "TRAVELCHANNEL", "TRAVELERS", "TRAVELERSINSURANCE", "TRUST", "TRV", "TT", "TUBE", "TUI", "TUNES", "TUSHU", "TV", "TVS", "TW", "TZ", "UA", "UBANK", "UBS", "UG", "UK", "UNICOM", "UNIVERSITY", "UNO", "UOL", "UPS", "US", "UY", "UZ", "VA", "VACATIONS", "VANA", "VANGUARD", "VC", "VE", "VEGAS", "VENTURES", "VERISIGN", "VERSICHERUNG", "VET", "VG", "VI", "VIAJES", "VIDEO", "VIG", "VIKING", "VILLAS", "VIN", "VIP", "VIRGIN", "VISA", "VISION", "VIVA", "VIVO", "VLAANDEREN", "VN", "VODKA", "VOLKSWAGEN", "VOLVO", "VOTE", "VOTING", "VOTO", "VOYAGE", "VU", "VUELOS", "WALES", "WALMART", "WALTER", "WANG", "WANGGOU", "WATCH", "WATCHES", "WEATHER", "WEATHERCHANNEL", "WEBCAM", "WEBER", "WEBSITE", "WED", "WEDDING", "WEIBO", "WEIR", "WF", "WHOSWHO", "WIEN", "WIKI", "WILLIAMHILL", "WIN", "WINDOWS", "WINE", "WINNERS", "WME", "WOLTERSKLUWER", "WOODSIDE", "WORK", "WORKS", "WORLD", "WOW", "WS", "WTC", "WTF", "XBOX", "XEROX", "XFINITY", "XIHUAN", "XIN", "XN--11B4C3D", "XN--1CK2E1B", "XN--1QQW23A", "XN--2SCRJ9C", "XN--30RR7Y", "XN--3BST00M", "XN--3DS443G", "XN--3E0B707E", "XN--3HCRJ9C", "XN--3OQ18VL8PN36A", "XN--3PXU8K", "XN--42C2D9A", "XN--45BR5CYL", "XN--45BRJ9C", "XN--45Q11C", "XN--4GBRIM", "XN--54B7FTA0CC", "XN--55QW42G", "XN--55QX5D", "XN--5SU34J936BGSG", "XN--5TZM5G", "XN--6FRZ82G", "XN--6QQ986B3XL", "XN--80ADXHKS", "XN--80AO21A", "XN--80AQECDR1A", "XN--80ASEHDB", "XN--80ASWG", "XN--8Y0A063A", "XN--90A3AC", "XN--90AE", "XN--90AIS", "XN--9DBQ2A", "XN--9ET52U", "XN--9KRT00A", "XN--B4W605FERD", "XN--BCK1B9A5DRE4C", "XN--C1AVG", "XN--C2BR7G", "XN--CCK2B3B", "XN--CG4BKI", "XN--CLCHC0EA0B2G2A9GCD", "XN--CZR694B", "XN--CZRS0T", "XN--CZRU2D", "XN--D1ACJ3B", "XN--D1ALF", "XN--E1A4C", "XN--ECKVDTC9D", "XN--EFVY88H", "XN--FCT429K", "XN--FHBEI", "XN--FIQ228C5HS", "XN--FIQ64B", "XN--FIQS8S", "XN--FIQZ9S", "XN--FJQ720A", "XN--FLW351E", "XN--FPCRJ9C3D", "XN--FZC2C9E2C", "XN--FZYS8D69UVGM", "XN--G2XX48C", "XN--GCKR3F0F", "XN--GECRJ9C", "XN--GK3AT1E", "XN--H2BREG3EVE", "XN--H2BRJ9C", "XN--H2BRJ9C8C", "XN--HXT814E", "XN--I1B6B1A6A2E", "XN--IMR513N", "XN--IO0A7I", "XN--J1AEF", "XN--J1AMH", "XN--J6W193G", "XN--JLQ61U9W7B", "XN--JVR189M", "XN--KCRX77D1X4A", "XN--KPRW13D", "XN--KPRY57D", "XN--KPU716F", "XN--KPUT3I", "XN--L1ACC", "XN--LGBBAT1AD8J", "XN--MGB9AWBF", "XN--MGBA3A3EJT", "XN--MGBA3A4F16A", "XN--MGBA7C0BBN0A", "XN--MGBAAKC7DVF", "XN--MGBAAM7A8H", "XN--MGBAB2BD", "XN--MGBAH1A3HJKRD", "XN--MGBAI9AZGQP6J", "XN--MGBAYH7GPA", "XN--MGBBH1A", "XN--MGBBH1A71E", "XN--MGBC0A9AZCG", "XN--MGBCA7DZDO", "XN--MGBCPQ6GPA1A", "XN--MGBERP4A5D4AR", "XN--MGBGU82A", "XN--MGBI4ECEXP", "XN--MGBPL2FH", "XN--MGBT3DHD", "XN--MGBTX2B", "XN--MGBX4CD0AB", "XN--MIX891F", "XN--MK1BU44C", "XN--MXTQ1M", "XN--NGBC5AZD", "XN--NGBE9E0A", "XN--NGBRX", "XN--NODE", "XN--NQV7F", "XN--NQV7FS00EMA", "XN--NYQY26A", "XN--O3CW4H", "XN--OGBPF8FL", "XN--OTU796D", "XN--P1ACF", "XN--P1AI", "XN--PBT977C", "XN--PGBS0DH", "XN--PSSY2U", "XN--Q7CE6A", "XN--Q9JYB4C", "XN--QCKA1PMC", "XN--QXA6A", "XN--QXAM", "XN--RHQV96G", "XN--ROVU88B", "XN--RVC1E0AM3E", "XN--S9BRJ9C", "XN--SES554G", "XN--T60B56A", "XN--TCKWE", "XN--TIQ49XQYJ", "XN--UNUP4Y", "XN--VERMGENSBERATER-CTB", "XN--VERMGENSBERATUNG-PWB", "XN--VHQUV", "XN--VUQ861B", "XN--W4R85EL8FHU5DNRA", "XN--W4RS40L", "XN--WGBH1C", "XN--WGBL6A", "XN--XHQ521B", "XN--XKC2AL3HYE2A", "XN--XKC2DL3A5EE0H", "XN--Y9A3AQ", "XN--YFRO4I67O", "XN--YGBI2AMMX", "XN--ZFR164B", "XXX", "XYZ", "YACHTS", "YAHOO", "YAMAXUN", "YANDEX", "YE", "YODOBASHI", "YOGA", "YOKOHAMA", "YOU", "YOUTUBE", "YT", "YUN", "ZA", "ZAPPOS", "ZARA", "ZERO", "ZIP", "ZM", "ZONE", "ZUERICH", "ZW" };
        public static int refreshTimerSeconds = 25;
        public bool isRefreshing = false;
        PerformanceCounter bytesSent;
        PerformanceCounter bytesReceived;

        CrawlSession thisSession;


        int currentCrawlSessionId;

        System.Windows.Threading.DispatcherTimer refreshTimer;

        List<Business> businesses;
        private FileHelperEngine<Business> fhInputEngine;
        private FileHelperEngine<outputBusiness> fhOutputEngine;
      
        #region Constructor

        public MainWindow()
        {
            
            InitializeComponent();
            businesses = new List<Business>();

            fhInputEngine = new FileHelperEngine<Business>();
            fhOutputEngine = new FileHelperEngine<outputBusiness>();
            bytesSent = new PerformanceCounter();
            bytesReceived = new PerformanceCounter();

        }

        #endregion

        #region New Crawl Session

        private void btnBrowseFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonOpenFileDialog openFileDialog = new CommonOpenFileDialog();
                if (openFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    csvText = File.ReadAllText(openFileDialog.FileName);
                    if (csvText.Length > 0)
                    {
                        //clean it.
                        csvText = cleanCSVText(csvText);
                        //parse it 
                        var businessesToAdd = fhInputEngine.ReadString(csvText);

                        businessesToAdd.ToList();
                        businesses.AddRange(businessesToAdd);

                        grdCrawlResults.ItemsSource = getCrawlGridDataFromBusinesses().AsDataView();
                        btnStartCrawl.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            


        }
        private void btnClearCsvInput_Click(object sender, RoutedEventArgs e)
        {
            grdCrawlResults.ItemsSource = null;
            btnBrowseFile.IsEnabled = true;
            btnStartCrawl.IsEnabled = false;
            businesses = new List<Business>();
            btnWriteOutput.IsEnabled = false;
            currentCrawlSessionId = -1;
            lblCompletedPercentage.Visibility = Visibility.Hidden;
            lblCompletedPercentage.Content = "";
            lblCrawled.Visibility = Visibility.Hidden;
            lblCrawled.Content = ""; 

        }
        private async void btnStartCrawl_Click(object sender, RoutedEventArgs e)
        {
            toggleAllButtons();


            thisSession = new CrawlSession();
            thisSession.startDate = DateTime.UtcNow;
            thisSession.totalPagesCrawled = 0;

            using (var ctx = new ApplicationDbContext())
            {
                foreach (Business item in businesses)
                {

                    ctx.CrawlSessions.Add(thisSession);

                    Models.CrawlResult thisOne = new Models.CrawlResult();
                    thisOne.city = item.City;
                    thisOne.phoneNumber = item.PhoneNumberCombined;
                    thisOne.BusinessName = item.CompanyName;
                    thisOne.address = item.Address;
                    thisOne.state = item.State;
                    thisOne.originatingWebsite = item.Website;
                    thisOne.timeFinished = DateTime.Parse(MIN_TIME);

                    thisOne.CompanyDescription = item.CompanyDescription;
                    thisOne.CorporateEmployeeSizeActual = item.CorporateEmployeeSizeActual;
                    thisOne.CorporateEmployeeSizeRange = item.CorporateEmployeeSizeRange;
                    thisOne.CorporateSalesVolumeActual = item.CorporateSalesVolumeActual;
                    thisOne.CorporateSalesVolumeRange = item.CorporateSalesVolumeRange;
                    thisOne.County = item.County;
                    thisOne.CreditScoreAlpha = item.CreditScoreAlpha;
                    thisOne.ExecutiveFirstName1 = item.ExecutiveFirstName1;
                    thisOne.ExecutiveFirstName2 = item.ExecutiveFirstName2;
                    thisOne.ExecutiveFirstName3 = item.ExecutiveFirstName3;
                    thisOne.ExecutiveGender1 = item.ExecutiveGender1;
                    thisOne.ExecutiveGender2 = item.ExecutiveGender2;
                    thisOne.ExecutiveGender3 = item.ExecutiveGender3;
                    thisOne.ExecutiveLastName1 = item.ExecutiveLastName1;
                    thisOne.ExecutiveLastName2 = item.ExecutiveLastName2;
                    thisOne.ExecutiveLastName3 = item.ExecutiveLastName3;
                    thisOne.ExecutiveTitle1 = item.ExecutiveTitle1;
                    thisOne.ExecutiveTitle2 = item.ExecutiveTitle2;
                    thisOne.ExecutiveTitle3 = item.ExecutiveTitle3;
                    thisOne.Facebook = item.Facebook;
                    thisOne.FranchiseDescription1 = item.FranchiseDescription1;
                    thisOne.FranchiseDescription2 = item.FranchiseDescription2;
                    thisOne.HomeBusiness = item.HomeBusiness;
                    thisOne.LinkedIn = item.LinkedIn;
                    thisOne.LocationEmployeeSizeActual = item.LocationEmployeeSizeActual;
                    thisOne.LocationEmployeeSizeRange = item.LocationEmployeeSizeRange;
                    thisOne.LocationSalesVolumeActual = item.LocationSalesVolumeActual;
                    thisOne.LocationSalesVolumeRange = item.LocationSalesVolumeRange;
                    thisOne.LocationType = item.LocationType;
                    thisOne.MetroArea = item.MetroArea;
                    thisOne.NAICS1 = item.NAICS1;
                    thisOne.NAICS1Description = item.NAICS1Description;
                    thisOne.NAICS2 = item.NAICS2;
                    thisOne.NAICS2Description = item.NAICS2Description;
                    thisOne.NAICS3 = item.NAICS3;
                    thisOne.NAICS3Description = item.NAICS3Description;
                    thisOne.Neighborhood = item.Neighborhood;
                    thisOne.PrimaryNAICS = item.PrimaryNAICS;
                    thisOne.PrimaryNAICSDescription = item.PrimaryNAICSDescription;
                    thisOne.PrimarySICAdSize = item.PrimarySICAdSize;
                    thisOne.PrimarySICCode = item.PrimarySICCode;
                    thisOne.PrimarySICDescription = item.PrimarySICDescription;
                    thisOne.PrimarySICYearAppeared = item.PrimarySICYearAppeared;
                    thisOne.SICCode1 = item.SICCode1;
                    thisOne.SICCode1Description = item.SICCode1Description;
                    thisOne.SICCode2 = item.SICCode2;
                    thisOne.SICCode2Description = item.SICCode2Description;
                    thisOne.SICCode3 = item.SICCode3;
                    thisOne.SICCode3Description = item.SICCode3Description;
                    thisOne.SquareFootage = item.SquareFootage;
                    thisOne.Twitter = item.Twitter;
                    thisOne.TypeofBusiness = item.TypeofBusiness;
                    thisOne.YearEstablished = item.YearEstablished;
                    thisOne.YearsInDatabase = item.YearsInDatabase;
                    

                    //add the result to the db context
                    ctx.CrawlResulsts.Add(thisOne);
                    //add the result to the colleciton
                    thisSession.CrawlResult.Add(thisOne);
                }
                ctx.SaveChanges();
            }
            currentCrawlSessionId = thisSession.CrawlSessionId;
            //// //asyncronously
            //Task.Run(() => startCrawler(currentCrawlSessionId));
            ////////New Method
            StartAbotCrawler(thisSession);

            refreshTimer = new DispatcherTimer();
            refreshTimer.Tick += refreshView;
            refreshTimer.Interval = new TimeSpan(0, 0, refreshTimerSeconds);
            refreshTimer.Start();

            // //syncronously
            //startCrawler(thisSession.CrawlSessionId);
        }

        private void btnWriteOutput_Click(object sender, RoutedEventArgs e)
        {
            writeOutputFileForSession(currentCrawlSessionId);

        }

        private void writeOutputFileForSession(int sessionId)
        {
            List<outputBusiness> toOutput = new List<outputBusiness>();
            using (var ctx = new ApplicationDbContext())
            {
                CrawlSession thisSession = ctx.CrawlSessions.Include("CrawlResult").Where(b => b.CrawlSessionId.Equals(sessionId)).FirstOrDefault();
                foreach (Models.CrawlResult item in thisSession.CrawlResult)
                {
                    outputBusiness addMe = new outputBusiness();
                    addMe.dateCrawled = thisSession.startDate.ToString();
                    addMe.address = item.address;
                    addMe.BusinessName = item.BusinessName;
                    addMe.city = item.city;

                    addMe.CompanyDescription = item.CompanyDescription;
                    addMe.CorporateEmployeeSizeActual = item.CorporateEmployeeSizeActual;
                    addMe.CorporateEmployeeSizeRange = item.CorporateEmployeeSizeRange;
                    addMe.CorporateSalesVolumeActual = item.CorporateSalesVolumeActual;
                    addMe.CorporateSalesVolumeRange = item.CorporateSalesVolumeRange;
                    addMe.County = item.County;
                    addMe.CreditScoreAlpha = item.CreditScoreAlpha;
                    addMe.ExecutiveFirstName1 = item.ExecutiveFirstName1;
                    addMe.ExecutiveFirstName2 = item.ExecutiveFirstName2;
                    addMe.ExecutiveFirstName3 = item.ExecutiveFirstName3;
                    addMe.ExecutiveGender1 = item.ExecutiveGender1;
                    addMe.ExecutiveGender2 = item.ExecutiveGender2;
                    addMe.ExecutiveGender3 = item.ExecutiveGender3;
                    addMe.ExecutiveLastName1 = item.ExecutiveLastName1;
                    addMe.ExecutiveLastName2 = item.ExecutiveLastName2;
                    addMe.ExecutiveLastName3 = item.ExecutiveLastName3;
                    addMe.ExecutiveTitle1 = item.ExecutiveTitle1;
                    addMe.ExecutiveTitle2 = item.ExecutiveTitle2;
                    addMe.ExecutiveTitle3 = item.ExecutiveTitle3;
                    addMe.Facebook = item.Facebook;
                    addMe.FranchiseDescription1 = item.FranchiseDescription1;
                    addMe.FranchiseDescription2 = item.FranchiseDescription2;
                    addMe.HomeBusiness = item.HomeBusiness;
                    addMe.LinkedIn = item.LinkedIn;
                    addMe.LocationEmployeeSizeActual = item.LocationEmployeeSizeActual;
                    addMe.LocationEmployeeSizeRange = item.LocationEmployeeSizeRange;
                    addMe.LocationSalesVolumeActual = item.LocationSalesVolumeActual;
                    addMe.LocationSalesVolumeRange = item.LocationSalesVolumeRange;
                    addMe.LocationType = item.LocationType;
                    addMe.MetroArea = item.MetroArea;
                    addMe.NAICS1 = item.NAICS1;
                    addMe.NAICS1Description = item.NAICS1Description;
                    addMe.NAICS2 = item.NAICS2;
                    addMe.NAICS2Description = item.NAICS2Description;
                    addMe.NAICS3 = item.NAICS3;
                    addMe.NAICS3Description = item.NAICS3Description;
                    addMe.Neighborhood = item.Neighborhood;
                    addMe.PrimaryNAICS = item.PrimaryNAICS;
                    addMe.PrimaryNAICSDescription = item.PrimaryNAICSDescription;
                    addMe.PrimarySICAdSize = item.PrimarySICAdSize;
                    addMe.PrimarySICCode = item.PrimarySICCode;
                    addMe.PrimarySICDescription = item.PrimarySICDescription;
                    addMe.PrimarySICYearAppeared = item.PrimarySICYearAppeared;
                    addMe.SICCode1 = item.SICCode1;
                    addMe.SICCode1Description = item.SICCode1Description;
                    addMe.SICCode2 = item.SICCode2;
                    addMe.SICCode2Description = item.SICCode2Description;
                    addMe.SICCode3 = item.SICCode3;
                    addMe.SICCode3Description = item.SICCode3Description;
                    addMe.SquareFootage = item.SquareFootage;
                    addMe.Twitter = item.Twitter;
                    addMe.TypeofBusiness = item.TypeofBusiness;
                    addMe.YearEstablished = item.YearEstablished;
                    addMe.YearsInDatabase = item.YearsInDatabase;


                    StringBuilder emailAddressesJoined = new StringBuilder();
                    foreach (CrawlEmail email in item.emails)
                    {
                        emailAddressesJoined.Append(email.emailAddress);
                        emailAddressesJoined.Append(" | ");

                    }
                    if (emailAddressesJoined.Length > 3)
                    {
                        emailAddressesJoined.Remove(emailAddressesJoined.Length - 2, 2);
                    }
                    addMe.emails = emailAddressesJoined.ToString();
                    addMe.originatingWebsite = item.originatingWebsite;
                    addMe.phoneNumber = item.phoneNumber;
                    addMe.state = item.state;

                    toOutput.Add(addMe);
                }
            }
            string path = System.IO.Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.MyDoc‌​uments), "CoffeeFilter", "Session" + sessionId.ToString() + ".csv");
            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.MyDoc‌​uments), "CoffeeFilter"));
            fhOutputEngine.WriteFile(path, toOutput.AsEnumerable());

        }

        private struct refreshReturnObj
        {
            public DataView results { get; set; }
            public bool completed { get; set; }
            public long totalPagesCrawled { get; set; }
            public int crawlResultCount { get; set; }
            public int completedTotal { get; set; }
        }
             

        private void refreshView(object sender, EventArgs e)
        {
            bool isComplete = false;
            using (var ctx = new ApplicationDbContext())
            {
                
                CrawlSession thisSession = ctx.CrawlSessions.Include("CrawlResult").Where(b => b.CrawlSessionId.Equals(currentCrawlSessionId)).FirstOrDefault();
                lblCrawled.Content = thisSession.totalPagesCrawled.ToString();
                isComplete = thisSession.completed;
            }

            if (isComplete == true)
            {
                //isRefreshing = true;
                Debug.WriteLine("/////////////////////////////////////start refresh: " + DateTime.UtcNow);
                lblCompletedPercentage.Visibility = Visibility.Visible;
                lblCrawled.Visibility = Visibility.Visible;
                //refreshTimer.Stop();

                Task t = Task.Factory.StartNew(() =>
                {
                    using (var ctx = new ApplicationDbContext())
                    {
                        int completedTotal = 0;
                        CrawlSession thisSession = ctx.CrawlSessions.Include("CrawlResult").Where(b => b.CrawlSessionId.Equals(currentCrawlSessionId)).FirstOrDefault();
                        DataView results = getCrawlDataFromCrawlResulstList(thisSession.CrawlResult.ToList(), thisSession, ref completedTotal).AsDataView();
                        refreshReturnObj toReturn = new refreshReturnObj();
                        toReturn.crawlResultCount = thisSession.CrawlResult.Count;
                        toReturn.totalPagesCrawled = thisSession.totalPagesCrawled;
                        toReturn.completed = thisSession.completed;
                        toReturn.completedTotal = completedTotal;
                        toReturn.results = results;
                        return toReturn;
                    }
                    
                }).ContinueWith(task =>
                {
                    grdCrawlResults.ItemsSource = task.Result.results;
                    if (task.Result.completed == true)
                    {
                        refreshTimer.Stop();
                        btnClearCsvInput.IsEnabled = true;
                        btnWriteOutput.IsEnabled = true;
                        lblCrawled.Content = "Total Crawled: ~" + task.Result.totalPagesCrawled.ToString() + "(Complete)";
                        lblCompletedPercentage.Content = "100%";
                    }
                    if (task.Result.completedTotal > 0)
                    {
                        double percentCoplete = task.Result.completedTotal / Convert.ToDouble(task.Result.crawlResultCount);
                        lblCompletedPercentage.Content = Math.Round(percentCoplete * 100).ToString() + "%";

                        //TimeSpan remaining = DateTime.UtcNow - thisSession.startDate;
                        //double remainingMinutes = (remaining.TotalSeconds * (100 / completedTotal)) / 60;
                        lblCrawled.Content = "Total Crawled: " + task.Result.totalPagesCrawled.ToString();
                    }
                    //refreshTimer.Start();
                    Debug.WriteLine("/////////////////////////////////////refresh complete: " + DateTime.UtcNow);
                    //isRefreshing = false;

                }, System.Threading.CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private DataTable getCrawlDataFromCrawlResulstList(List<Models.CrawlResult> resultsSoFar, CrawlSession thisSession, ref int completedTotal)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("businessName");
            dt.Columns.Add("city");
            dt.Columns.Add("state");
            dt.Columns.Add("originatingWebsite");
            dt.Columns.Add("completed");
            dt.Columns.Add("emails");
            
            DataRow dr = null;
            foreach (var result in resultsSoFar)
            {
                dr = dt.NewRow();
                dr["businessName"] = result.BusinessName;
                dr["city"] = result.city;
                dr["state"] = result.state;
                dr["originatingWebsite"] = result.originatingWebsite;
                if(result.completed == true)
                {
                    TimeSpan timeSinceStart = result.timeFinished - thisSession.startDate;
                    dr["completed"] = timeSinceStart.ToString(@"hh\:mm\:ss", new CultureInfo("en-US"));
                    completedTotal += 1;
                } else
                {
                    dr["completed"] = "";
                }
               

                StringBuilder emailAddressesJoined = new StringBuilder();
                IEnumerable<CrawlEmail> distinctEmails = result.emails.GroupBy(x => x.emailAddress).Select(x => x.First());
                foreach (CrawlEmail email in distinctEmails)
                {
                    emailAddressesJoined.Append(email.emailAddress);
                    emailAddressesJoined.Append(", ");

                }
                if (emailAddressesJoined.Length > 3)
                {
                    emailAddressesJoined.Remove(emailAddressesJoined.Length - 2, 2);
                }
                dr["emails"] = emailAddressesJoined.ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private DataTable getCrawlGridDataFromBusinesses()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("businessName");
            dt.Columns.Add("city");
            dt.Columns.Add("state");
            dt.Columns.Add("originatingWebsite");
            dt.Columns.Add("completed");
            dt.Columns.Add("emails");

            DataRow dr = null;
            foreach (var business in businesses)
            {
                dr = dt.NewRow();
                dr["businessName"] = business.CompanyName;
                dr["city"] = business.City;
                dr["state"] = business.State;
                dr["originatingWebsite"] = business.Website;
                dr["completed"] = "";
                dr["emails"] = "";
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void toggleAllButtons()
        {
            btnStartCrawl.IsEnabled = !btnStartCrawl.IsEnabled;
            btnClearCsvInput.IsEnabled = !btnClearCsvInput.IsEnabled;
            btnBrowseFile.IsEnabled = !btnBrowseFile.IsEnabled;
        }

        private string cleanCSVText(string input)
        {
            string cleanedString = input;

            var regex = new Regex("\\\"(.*?)\\\"");
            cleanedString = regex.Replace(cleanedString, m => m.Value.Replace(',', '@'));

            cleanedString = cleanedString.Replace("\"", "");
            return cleanedString;
        }

        #endregion New Crawl Session

        #region Do the crawling
        private static void startCrawler(int crawlSessionId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //CrawlSession thisSession = (from t in ctx.CrawlSessions
                //                           where t.CrawlSessionId == crawlSessionId
                //                           select t).First();
                CrawlSession thisSession = ctx.CrawlSessions.Include("CrawlResult").Where(b => b.CrawlSessionId.Equals(crawlSessionId)).FirstOrDefault();

                foreach (Models.CrawlResult item in thisSession.CrawlResult)
                {
                    IEnumerable<Models.CrawlResult> alreadyExists = (from r in ctx.CrawlResulsts
                                                              where r.originatingWebsite == item.originatingWebsite
                                                              where r.completed == true
                                                              select r);
                    if (alreadyExists.Count() > 0)
                    {
                        var foundAlready = alreadyExists.First();
                        foreach (CrawlEmail email in alreadyExists.First().emails)
                        {
                            CrawlEmail toAdd = new CrawlEmail();
                            toAdd.emailAddress = email.emailAddress;
                            toAdd.sourceUrl = email.sourceUrl;
                            item.emails.Add(toAdd);
                        }
                    }
                    else
                    {
                        var url = "http://" + item.originatingWebsite.ToLower();

                        item.baseUrl = System.Text.RegularExpressions.Regex.Replace(url, @"(http(s)?:\/\/)|(\/.*){1}", "").ToLower();


                        processPage(item, url, ctx);
                        thisSession.bytesReceived = thisSession.bytesReceived + item.bytesReceived;

                    }
                    //Debug.WriteLine("/////////////////////////////////////setting PAGE to complete");
                    item.completed = true;
                    ctx.SaveChanges();


                }
                thisSession.completed = true;
                //Debug.WriteLine("/////////////////////////////////////setting SESSION to complete");
                ctx.SaveChanges();

            }


        }

        public static void processPage(Models.CrawlResult thisSitesResults, string urlToProcess, ApplicationDbContext ctx)
        {
            try
            {
                //dont bother scanning it if it is in the black list of urls to scan.
                var ignoreList = urlsToIgnore.Union(exdludedEmailExtensions);
                if (urlsToIgnore.Any(s => urlToProcess.Contains(s)))
                {
                    return;
                }
                urlToProcess = cleanUrl(urlToProcess);
                //Debug.WriteLine(urlToProcess);

                var httpClient = new HttpClient();
                string rebuiltUrl = "https://" + urlToProcess;
                if (!thisSitesResults.isHttps)
                {
                    rebuiltUrl = "http://" + urlToProcess;
                }

                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, rebuiltUrl);

                req.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:74.0) Gecko/20100101 Firefox/74.0");

                Task<HttpResponseMessage> html = httpClient.SendAsync(req);

                string fullHtml = html.Result.Content.ReadAsStringAsync().Result;
                //get the content length and add it to the total.
                if (html.Result.Content.Headers.ContentLength != null)
                {
                    thisSitesResults.bytesReceived = thisSitesResults.bytesReceived + Convert.ToInt64(html.Result.Content.Headers.ContentLength);
                }

                string currentReqUri = html.Result.RequestMessage.RequestUri.ToString();
                List<string> allUrlsToProcess = new List<string>();

                if (!thisSitesResults.allLinkedUrlsInDomain.Contains(urlToProcess))
                {
                    thisSitesResults.allLinkedUrlsInDomain.Add(urlToProcess);
                    if (thisSitesResults.allLinkedUrlsInDomain.Count == 1)
                    {
                        //this was the first request, determine if https;
                        System.Text.RegularExpressions.Match theMatch = System.Text.RegularExpressions.Regex.Match(currentReqUri, @"(http(s)?:\/\/)");
                        if (theMatch.Value.Contains("s"))
                        {
                            thisSitesResults.isHttps = true;
                        }

                        //this is the tld. process all links on this page and no deeper (for speed sake)

                        allUrlsToProcess = getAllUrlsFromPage(fullHtml, thisSitesResults);
                    }
                }



                foreach (string match in allUrlsToProcess)
                {
                    string cleanedMatch = cleanUrl(match).ToLower();

                    if (cleanedMatch.Contains(thisSitesResults.baseUrl))
                    {
                        //if it's in the same domain, crawl it.                    
                        if (!thisSitesResults.allLinkedUrlsInDomain.Contains(cleanedMatch))
                        {
                            thisSitesResults.allLinkedUrlsInDomain.Add(cleanedMatch);
                            //Recurse do to that page too!
                            ctx.SaveChanges();
                            processPage(thisSitesResults, cleanedMatch, ctx);
                        }
                    }
                }

                System.Text.RegularExpressions.MatchCollection foundEmails = System.Text.RegularExpressions.Regex.Matches(fullHtml, betterEmailRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                foreach (System.Text.RegularExpressions.Match match in foundEmails)
                {
                    string extension = match.Value.Substring(match.Value.LastIndexOf('.') + 1).ToUpper();

                    if (allTLDs.Any(s => extension.Contains(s)))
                    {
                        if (match.Value.StartsWith("/") != true)
                        {
                            var foundEmail = thisSitesResults.emails.Where(b => b.emailAddress.ToLower().Equals(match.Value.ToLower()));
                            if (foundEmail.Count() == 0)
                            {
                                CrawlEmail newEmail = new CrawlEmail();
                                newEmail.emailAddress = match.Value.ToLower();
                                newEmail.sourceUrl = urlToProcess.ToLower();
                                newEmail.CrawlResult = thisSitesResults;
                                //add the email to the crawlResult
                                thisSitesResults.emails.Add(newEmail);

                                //add this email to the context.
                                ctx.CrawlEmails.Add(newEmail);

                                ctx.SaveChanges();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //that one failed, move on.

            }
        }

        public static List<string> getAllUrlsFromPage(string fullHtml, Models.CrawlResult thisSitesResults)
        {
            System.Text.RegularExpressions.MatchCollection fullyQualifiedUrls = System.Text.RegularExpressions.Regex.Matches(fullHtml, urlRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.MatchCollection relativeUrls = System.Text.RegularExpressions.Regex.Matches(fullHtml, relativeUrlRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //create a list of all the url's. Building up the relatives urls so they can be navigated to.
            List<string> fullUrlList = new List<string>();
            foreach (System.Text.RegularExpressions.Match item in fullyQualifiedUrls)
            {
                fullUrlList.Add(item.Value);
            }
            string urlBeginning = "http://";
            if (thisSitesResults.isHttps == true)
            {
                urlBeginning = "https://";
            }
            foreach (System.Text.RegularExpressions.Match item in relativeUrls)
            {
                string cleanedRelativeUrl = item.Value.Replace("href=\"", "").Replace("\"", "");

                fullUrlList.Add(urlBeginning + thisSitesResults.baseUrl + cleanedRelativeUrl);
            }

            //limit the number of urls.
            //order them by length and take the top 15;
            fullUrlList = (from u in fullUrlList
                           where u.Contains(thisSitesResults.baseUrl)
                           orderby u.Length ascending
                           select u).Take(NUMBER_OF_URLS_TO_CRAWL).ToList();

            return fullUrlList;
        }

        public static string cleanUrl(string original)
        {
            string cleanedMatch = System.Text.RegularExpressions.Regex.Replace(original, @"(http(s)?:\/\/)", "");
            cleanedMatch = cleanedMatch.TrimEnd('/');
            return cleanedMatch;
        }

        #endregion Do the crawling    

        #region Crawling with Abot Package
        private static async Task StartAbotCrawler(Models.CrawlSession thisSession)
        {
            using (var ctx = new ApplicationDbContext())
            {
                foreach (Models.CrawlResult item in thisSession.CrawlResult)
                {
                    IEnumerable<Models.CrawlResult> alreadyExists = (from r in ctx.CrawlResulsts
                                                                     where r.originatingWebsite == item.originatingWebsite
                                                                     where r.completed == true
                                                                     select r);
                    Models.CrawlResult contextItem = (from c in ctx.CrawlResulsts
                                                      where c.CrawlResultId == item.CrawlResultId
                                                      select c).First();
                    if (alreadyExists.Count() > 0)
                    {
                        var foundAlready = alreadyExists.First();
                        foreach (CrawlEmail email in alreadyExists.First().emails)
                        {
                            CrawlEmail toAdd = new CrawlEmail();
                            toAdd.emailAddress = email.emailAddress;
                            toAdd.sourceUrl = email.sourceUrl;
                            contextItem.emails.Add(toAdd);
                        }

                        contextItem.completed = true;
                        contextItem.timeFinished = DateTime.UtcNow;
                        ctx.SaveChanges();
                    }
                    else
                    {
                        var url = "http://" + item.originatingWebsite.ToLower();

                        item.baseUrl = System.Text.RegularExpressions.Regex.Replace(url, @"(http(s)?:\/\/)|(\/.*){1}", "").ToLower();


                        await AbotCrawler(url, thisSession, item);
                        thisSession.bytesReceived = thisSession.bytesReceived + item.bytesReceived;

                    }
                }
                
            }

            using (var ctx = new ApplicationDbContext())
            {
                CrawlSession current = ctx.CrawlSessions.Include("CrawlResult").Where(b => b.CrawlSessionId.Equals(thisSession.CrawlSessionId)).FirstOrDefault();
                current.completed = true;
                Debug.WriteLine("/////////////////////////////////////setting SESSION to complete");
                ctx.SaveChanges();
            }


        }
        private static async Task AbotCrawler(string url, CrawlSession thisSession, Models.CrawlResult thisResult)
        {
            var config = new CrawlConfiguration
            {
                MaxPagesToCrawl = 10, //Only crawl 20 pages
                MinCrawlDelayPerDomainMilliSeconds = 200 //Wait this many millisecs between requests
            };
            var threadManager = new TaskThreadManager(2);

            var crawler = new PoliteWebCrawler(config, null, threadManager, null, null, null, null, null, null);

           
            crawler.CrawlBag.session = thisSession;
            crawler.CrawlBag.result = thisResult;
            
            crawler.PageCrawlCompleted += PageCrawlCompleted;//Several events available...

            var crawlResult = await crawler.CrawlAsync(new Uri(url));

            using (var ctx = new ApplicationDbContext())
            {
                Models.CrawlResult result = ctx.CrawlResulsts.Where(b => b.CrawlResultId.Equals(thisResult.CrawlResultId)).First();
                result.completed = true;
                result.timeFinished = DateTime.UtcNow;
                ctx.SaveChanges();
            }

        }

        private static void PageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            int crawlSessionId = e.CrawlContext.CrawlBag.session.CrawlSessionId;
            int crawlResultId = e.CrawlContext.CrawlBag.result.CrawlResultId;
            if (e.CrawledPage.HttpResponseMessage != null)
            {
                var httpStatus = e.CrawledPage.HttpResponseMessage.StatusCode;
                var rawPageText = e.CrawledPage.Content.Text;
                
                e.CrawledPage.PageBag.emails = new List<CrawlEmail>();

                findAndAddEmails(rawPageText, e);
                using (var ctx = new ApplicationDbContext())
                {
                    CrawlSession thisSession = ctx.CrawlSessions.Include("CrawlResult").Where(b => b.CrawlSessionId.Equals(crawlSessionId)).FirstOrDefault();
                    thisSession.totalPagesCrawled += 1;
                    //.Dispatcher.BeginInvoke(new Action(() =>
                    //{

                    //}), DispatcherPriority.Background);
                    Models.CrawlResult thisResult = ctx.CrawlResulsts.Where(b => b.CrawlResultId.Equals(crawlResultId)).First();
                    foreach (CrawlEmail item in e.CrawledPage.PageBag.emails)
                    {
                        CrawlEmail addMe = new CrawlEmail();
                        addMe.CrawlResult = thisResult;
                        addMe.emailAddress = item.emailAddress;
                        addMe.sourceUrl = item.sourceUrl;
                        thisResult.emails.Add(addMe);
                    }
                    ctx.SaveChanges();
                }
            } else
            {
                //TODO: Extend to include page failed to crawl message.
                using (var ctx = new ApplicationDbContext())
                {
                    Models.CrawlResult thisResult = ctx.CrawlResulsts.Where(b => b.CrawlResultId.Equals(crawlResultId)).First();
                    thisResult.completed = true;
                    ctx.SaveChanges();
                }
            }
            

        }

        private static void findAndAddEmails(string pageText, PageCrawlCompletedArgs e)
        {
            System.Text.RegularExpressions.MatchCollection foundEmails = System.Text.RegularExpressions.Regex.Matches(pageText, betterEmailRegex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            foreach (System.Text.RegularExpressions.Match match in foundEmails)
            {
                string extension = match.Value.Substring(match.Value.LastIndexOf('.') + 1).ToUpper();

                if (allTLDs.Any(s => extension.Contains(s)))
                {
                    if (match.Value.StartsWith("/") != true)
                    {
                        //var foundEmail = result.emails.Where(b => b.emailAddress.ToLower().Equals(match.Value.ToLower()));
                        //if (foundEmail.Count() == 0)
                        //{
                            CrawlEmail newEmail = new CrawlEmail();
                            newEmail.emailAddress = match.Value.ToLower();
                            newEmail.sourceUrl = e.CrawledPage.Uri.AbsoluteUri.ToLower();
                            
                            //add the email to the crawlResult
                            e.CrawledPage.PageBag.emails.Add(newEmail);

                            //add this email to the context.
                            //ctx.CrawlEmails.Add(newEmail);

                            //ctx.SaveChanges();
                        //}
                    }

                }
            }
        }

        #endregion Crawling with Abot Package

    }
}