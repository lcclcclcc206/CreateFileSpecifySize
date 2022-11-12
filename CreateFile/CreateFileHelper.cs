namespace CreateFile
{
    public enum Unit
    {
        B,
        KB,
        MB,
        GB
    }
    public class CreateFileHelper
    {
        public static long GetUnitValue(Unit unit) =>
            unit switch
            {
                Unit.B => 1,
                Unit.KB => (long)Math.Pow(2, 10),
                Unit.MB => (long)Math.Pow(2, 20),
                Unit.GB => (long)Math.Pow(2, 30),
                _ => throw new ArgumentException("Invalid enum value for unit", nameof(unit)),
            };

        public static async Task<string> CreateFileAsync(FileInfo file, int size, Unit unit)
        {
            long fileSize = size * GetUnitValue(unit);

            using (FileStream fs = new(file.FullName, FileMode.Create))
            {
                int blockSize = 1024 * 1024;
                long capacity = fileSize;
                byte[] block = new byte[blockSize];
                while ((capacity - blockSize) > 0)
                {
                    await fs.WriteAsync(block, 0, blockSize);
                    capacity -= blockSize;
                }
                await fs.WriteAsync(block, 0, (int)capacity);
            }

            string result = $"Create file success!\nFile path: {file.FullName}\nSize: {size}{unit}";
            return result;
        }

    }
}