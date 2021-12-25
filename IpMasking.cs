using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpMaskingLogic
{
    public class IpMasking
    {
        // Creating a masked ip address for each ip in the forwarded list. 
        public void createIpMasking(ref List<string> outputIp)
        {

            // creating a copy of the original ip list
            string[] ipCopy = new string[outputIp.Count()];

            for (int i = 0; i < outputIp.Count(); i++)
            {
                ipCopy[i] = outputIp[i];
            }

            //find the list size
            var size = outputIp.Count();

            // initiating count variable to check if there are ip addresses in the same network 
            var count = 0;

            //iterating over the ip octets to find ip addresses on the same network
            //and creating randomized masked ip that shares the same network address
            for (int i = 0; i < size - 1; i++)
            {
                for (int j = i + 1; j <= size - 1; j++)
                {
                    //find the ip addresses octets
                    var ip1 = outputIp[i].Split('.');
                    var ip2 = outputIp[j].Split('.');

                    for (int k = 0; k < 3; k++)
                    {
                        //checking if the network octets equal to each other
                        if (ip1[k] == ip2[k])
                        {
                            count++;
                        }
                        else
                        {
                            count = 0;
                        }
                    }

                    if (count == 3)
                    {
                        // creating random ip address
                        var random = new Random();
                        string randomIp = $"{random.Next(192, 223)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}";

                        //random number octets
                        var randomIpOcts = randomIp.Split('.');

                        //creating random numbers
                        var randomNum1 = random.Next(0, 255);
                        var randomNum2 = random.Next(0, 255);

                        // iterating over the ip addresses octets found on the same network and assigning the random octets to them
                        for (int l = 0; l < 3; l++)
                        {
                            ip1[l] = randomIpOcts[l];
                            ip2[l] = randomIpOcts[l];
                        }

                        // assigning random number for the last octet in the address
                        ip1[3] = randomNum1.ToString();
                        ip2[3] = randomNum2.ToString();

                        //assigning the new created masked ip addresses to the original ip addresses
                        outputIp[i] = string.Join(".", ip1);
                        outputIp[j] = string.Join(".", ip2);

                    }
                }
            }

            //iterating over the list of the ip addressses to find ip addresses which haven't been masked yet.
            for (int i = 0; i < outputIp.Count(); i++)
            {
                //checking if same ip addresses exist both in the original ip addresses list and the copied one.
                if (ipCopy[i] == outputIp[i])
                {
                    //if exist generating random number for the unmasked ip address
                    var randomClass = new Random();
                    string randIp = $"{randomClass.Next(192, 223)}.{randomClass.Next(0, 255)}.{randomClass.Next(0, 255)}.{randomClass.Next(0, 255)}";

                    // replacing between the original ip address and the masked one
                    outputIp[i] = outputIp[i].Replace(outputIp[i], randIp);
                }
            }
        }
    }
}

