using System;
using System.Collections.Generic;

namespace Subdomain_Visit_Count
{
  class Program
  {
    static void Main(string[] args)
    {
      Program p = new Program();
      var cpdomains = new string[] { "900 google.mail.com", "50 yahoo.org", "1 intel.mail.com", "5 wiki.org", "1 mail.com" };
      var result = p.SubdomainVisits(cpdomains);
      Console.WriteLine(string.Join(",", result));
    }

    public IList<string> SubdomainVisits(string[] cpdomains)
    {
      List<string> result = new List<string>();
      Dictionary<string, int> reference = new Dictionary<string, int>();
      foreach (string cpdomain in cpdomains)
      {
        if(cpdomain.Contains(" "))
        {
          var arr = cpdomain.Split(" ");
          int count;
          int.TryParse(arr[0], out count);
          string domain = arr[1];
          UpdateDictionary(reference, domain, count);
          if (domain.Contains("."))
          {
            var subDomains = domain.Split(".");
            if(subDomains.Length == 2) 
            {
              domain = subDomains[1];
              UpdateDictionary(reference, domain, count);
            }
            if (subDomains.Length == 3)
            {
              domain = subDomains[2];
              UpdateDictionary(reference, domain, count);

              domain = $"{subDomains[1]}.{subDomains[2]}";
              UpdateDictionary(reference, domain, count);
            }
          }
        }
      }

      foreach(var item in reference)
      {
        result.Add($"{item.Value} {item.Key}");
      }
      return result;
    }

    void UpdateDictionary(Dictionary<string, int> reference, string key, int value)
    {
      if (reference.ContainsKey(key))
        reference[key] = reference[key] + value;
      else
        reference.Add(key, value);
    }
  }
}
