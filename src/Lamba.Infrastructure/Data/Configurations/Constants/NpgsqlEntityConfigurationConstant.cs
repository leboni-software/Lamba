using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Infrastructure.Data.Configurations.Constants
{
    public static class NpgsqlEntityConfigurationConstant
    {
        public const string DefaultDateTimeUTC = "now() AT TIME ZONE 'UTC'";
        public const string DefaultRandomUUID = "gen_random_uuid()";
        public const string DeletedFilter = "\"DeletedAt\" IS NULL";
    }
}
