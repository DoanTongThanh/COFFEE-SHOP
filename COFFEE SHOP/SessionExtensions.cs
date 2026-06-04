using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace COFFEE_SHOP // Hãy đảm bảo trùng với Namespace dự án của bạn
{
    public static class SessionExtensions
    {
        // 1. Hàm dùng để biến Object thành JSON rồi lưu vào Session
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // 2. Hàm lấy chuỗi JSON từ Session ra rồi dịch ngược thành Object
        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}