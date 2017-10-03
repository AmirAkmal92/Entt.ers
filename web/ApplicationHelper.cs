using System.Collections.Generic;

namespace Entt.Ers
{
    public class ApplicationHelper
    {
        public static Dictionary<string, string> GetBranches()
        {
            var list = new Dictionary<string, string>
            {
                { "7185", "Pusat POS Laju Nilai" },
                { "4038", "Commercial Courier" },
                { "6101", "HUB POS Laju Domestik KLIA" },
                { "6225", "Pusat POS Laju Putrajaya" },
                { "1000", "Pusat POS Laju Penang" }
            };
            return list;
        }

        public static Dictionary<string, string> Ranks()
        {
            var list = new Dictionary<string, string>
            {
                { "LAKSDYA", "Laksamana Madya" },
                { "LAKSDA", "Laksamana Muda" },
                { "LAKSMA", "Laksamana Pertama" },
                { "KEPT", "Kepten" },
                { "KDR", "Komander" },
                { "LT KDR", "Leftenan Komander" },
                { "LT", "Leftenan" },
                { "LT DYA", "Leftenan Madya" },
                { "LT MDA", "Leftenan Muda" },
                { "PKK", "Pegawai Kadet Kanan" },
                { "KDT", "Kadet" }
            };
            return list;
        }

        public static List<string> AcademicQualifications()
        {
            var list = new List<string>
            {
                "Sarjana Kedoktoran",
                "Sarjana",
                "Sarjana Muda",
                "Diploma",
                "Sijil/Matrikulasi",
                "STPM",
                "SPM",
                "SRP",
                "Tiada"
            };
            return list;
        }

        public static List<string> BloodTypes()
        {
            var list = new List<string>
            {
                "O+",
                "O",
                "O-",
                "A+",
                "A",
                "A-",
                "B+",
                "B",
                "B-",
                "AB+",
                "AB",
                "AB-"
            };
            return list;
        }

        public static List<string> Industries()
        {
            var list = new List<string>
            {
                "Agriculture",
                "Automotive",
                "Aviation",
                "Banking/Finance ",
                "Construction/Development",
                "Contact Centre",
                "Education/Training",
                "Engineering",
                "Food & Beverage",
                "Government Sector",
                "Hospitality",
                "Information Technology & Computing",
                "Insurance",
                "Investment",
                "Legal",
                "Maintenance & Servicing Industry",
                "Manufacturing",
                "Media & Publishing",
                "Medical/Healtcare",
                "Oil & Gas",
                "Retail",
                "Security & Safety",
                "Self Employed",
                "Shipping",
                "Sports",
                "Telecommunication",
                "Tourism",
                "Transportation & Logistics",
            };
            return list;
        }

        public static List<string> OccupationCategories()
        {
            var list = new List<string>
            {
                "Accounting",
                "Acturial/Statistics",
                "Advertising/Creative Design",
                "Agent",
                "Architect/Interior Designer",
                "Auditor",
                "Bio Technologist",
                "Chef/Cook",
                "Clerical/General Admin",
                "Consultant",
                "Counter Services",
                "Customer Service",
                "Editor",
                "Engineer-Maintenance",
                "Engineering-Aviation",
                "Engineering-Telecommunication",
                "Engineering-Agriculture",
                "Engineering-Chemical",
                "Engineering-Electrical",
                "Engineering-Electronic",
                "Engineering-Environmental",
                "Engineering-Industry Engineering",
                "Engineering-Marine",
                "Engineering-Mechnical/Automotive",
                "Engineering-Others",
                "Entertainment",
                "General Worker",
                "Geology/Geophysics",
                "Helpdesk/Frontdesk",
                "Human Resources",
                "Inspection Officer",
                "IT-Hardware",
                "IT-Network/System/Database",
                "IT-Software",
                "Journalist",
                "Judge",
                "Labotaries",
                "Lawyer/Legal Service",
                "Lecturer",
                "Marketing Support",
                "Material Control",
                "Medical-Doctor",
                "Medical-Nurse",
                "Medical-Support",
                "Medical-Surgeon",
                "Operation Support-Site",
                "Operational Support-Office",
                "Pilot",
                "Principle",
                "Process Control",
                "Professional Player",
                "Professor",
                "Public Relation",
                "Quality Control",
                "Quantity Surveying",
                "Real Estate",
                "Sales/Marketing",
                "Sales Support",
                "Secretarial",
                "Ship-General Worker",
                "Ship Captain",
                "Ship-Officer",
                "Social Service",
                "Sportsman/Sportswomen",
                "Teacher",
                "Technician",
                "Telemarketing/Telesales",
                "Top Level Management",
                "Trainer",
                "Vetenarian",
                "Others"
            };
            return list;
        }

        public static List<string> MaritalStatuses()
        {
            var list = new List<string>
            {
                "Bujang",
                "Kahwin"
            };
            return list;
        }

        public static List<string> SwimmingStatues()
        {
            var list = new List<string>
            {
                "Lulus",
                "Gagal"
            };
            return list;
        }

        public static List<string> MalaysiaStates()
        {
            var list = new List<string>
            {
                "Johor",
                "Kedah",
                "Kelantan",
                "Melaka",
                "Negeri Sembilan",
                "Pahang",
                "Perak",
                "Perlis",
                "Pulau Pinang",
                "Sabah",
                "Sarawak",
                "Selangor",
                "Terengganu",
                "W.P Kuala Lumpur",
                "W.P Labuan",
                "W.P Putrajaya"
            };
            return list;
        }

        public static List<string> Genders()
        {
            var list = new List<string>
            {
                "Male",
                "Female"
            };
            return list;
        }
    }
}