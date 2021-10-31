using System;
using System.Text;

namespace NotificacionApp.Common
{
    /// <summary>
    /// Security token.
    /// </summary>
    public class SecurityToken
    {
        private const string HashSeperator = ".";
        private const string FieldSeperator = "|";

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="userRole"></param>
        public SecurityToken(Guid id, string name, UserRole userRole)
        {
            Id = id;
            Name = name;
            UserRole = userRole;
        }

        /// <summary>
        /// Guid.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// User role.
        /// </summary>
        public UserRole UserRole { get; private set; }

        /// <summary>
        /// Time.
        /// </summary>
        public DateTime Time { get; private set; }

        /// <summary>
        /// Hash value.
        /// </summary>
        /// <returns>Hash value of the token</returns>
        public string Hash()
        {
            var now = DateTime.UtcNow.Ticks;
            var text = $"{Id}{FieldSeperator}{Name}{FieldSeperator}{(int)UserRole}{FieldSeperator}{now}";
            var hashValue = EncryptionHelper.Hash(text);
            var encryptedKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));

            return $"{encryptedKey}.{hashValue}";
        }

        /// <summary>
        /// Validate.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SecurityToken? Validate(string value)
        {
            try
            {
                if (!value.Contains(HashSeperator))
                {
                    return null;
                }

                var key = value.Split(HashSeperator)[0];
                var hash = value.Split(HashSeperator)[1];
                var text = Encoding.UTF8.GetString(Convert.FromBase64String(key));

                if (!EncryptionHelper.Verify(text, hash))
                {
                    return null;
                }

                if (!text.Contains(FieldSeperator))
                {
                    return null;
                }

                var fields = text.Split(FieldSeperator);
                if (fields.Length < 4)
                {
                    return null;
                }

                var indexId = 0;
                var indexTime = fields.Length - 1;
                var indexUserRole = fields.Length - 2;
                var indexTextBegin = fields[indexId].Length + 1;
                var indexTextLength = text.Length - (fields[indexId].Length + 1) - (fields[indexTime].Length + 1) - (fields[indexUserRole].Length + 1);

                var textId = fields[indexId];
                var textTime = fields[indexTime];
                var textUserRole = fields[indexUserRole];



                var id = new Guid(textId);
                var time = new DateTime(long.Parse(textTime));
                var userRole = (UserRole)int.Parse(textUserRole);
                var name = text.Substring(indexTextBegin, indexTextLength);

                return new SecurityToken(id, name, userRole)
                {
                    Time = time
                };
            }
            catch
            {
                return null;
            }
        }


    }
}
