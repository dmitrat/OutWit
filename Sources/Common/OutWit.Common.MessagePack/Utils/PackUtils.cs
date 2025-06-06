﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutWit.Common.MessagePack.Utils
{
    public static class PackUtils
    {
        #region Export
#if NET6_0_OR_GREATER
        public static async Task ExportAsMessagePackAsync<T>(this IEnumerable<T> me, string filePath)
        {
            var bytes = me.ToArray().ToPackBytes();
            if (bytes != null)
                await File.WriteAllBytesAsync(filePath, bytes);
        }
#endif
        public static void ExportAsMessagePack<T>(this IEnumerable<T> me, string filePath)
        {
            var bytes = me.ToArray().ToPackBytes();
            if (bytes != null)
                File.WriteAllBytes(filePath, bytes);
        }

        #endregion

        #region Load
#if NET6_0_OR_GREATER
        public static async Task<IReadOnlyList<T>> LoadAsMessagePackAsync<T>(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            var bytes = await File.ReadAllBytesAsync(filePath);

            return bytes.FromPackBytes<IReadOnlyList<T>>();
        }
#endif
        public static IReadOnlyList<T> LoadAsMessagePack<T>(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            var bytes = File.ReadAllBytes(filePath);

            return bytes.FromPackBytes<IReadOnlyList<T>>();
        }

        #endregion
    }
}
