namespace Juntos.Helper
{
    public class GeneralHelper
    {

        // UnpackMemberships : Splits allowed membership string to ids
        public int[] UnpackMemberships(string membershipString)
        {
            int[] membershipIds = Array.Empty<int>();

            if (membershipString != null)
            {
                string[] membershipStringSplit = membershipString.Split(";");

                for (int i = 0; i < membershipStringSplit.Length; i++)
                {
                    if (Int32.TryParse(membershipStringSplit[i], out int id) == true)
                    {
                        membershipIds.Append(id);
                    }
                }
            }

            return membershipIds;
        }

        // PackMemberships : Packs membership array to string
        public string PackMemberships(int[] membershipIds)
        {
            string packedMemberships = string.Empty;

            if (membershipIds != null)
            {
                for (int i = 0; i < membershipIds.Length; i++)
                {
                    string convertedId = membershipIds[i].ToString();
                    if (convertedId != null)
                    {
                        packedMemberships += convertedId + ";";
                    }
                }
            }
            return packedMemberships;
        }

        // ValidateMembership : Checks if a membership is valid 
        public bool ValidateMembership(int membershipId, string validMemberships)
        {
            int[] validIds = UnpackMemberships(validMemberships);

            bool isValid = false;
            for (int i = 0; i < validIds.Length; i++)
            {
                if (validIds[i] == membershipId)
                {
                    isValid = true;
                    break;
                }
            }

            return isValid;
        }
    }
}