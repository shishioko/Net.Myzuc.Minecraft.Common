using System.Collections.Specialized;

namespace Net.Myzuc.Minecraft.Common.Objects.Data
{
    /*public sealed class PalettedArray
    {
        private int Length { get; }
        private int IndirectStartBits { get; }
        private int IndirectEndBits { get; }
        private int DirectStartBits { get; }
        private OrderedDictionary<int, int> Palette { get; set; }
        private PackedArray? Container { get; set; }

        public PalettedArray(int length, int indirectStartBits, int indirectEndBits, int directStartBits)
        {
            Length = length;
            IndirectStartBits = indirectStartBits;
            IndirectEndBits = indirectEndBits;
            DirectStartBits = directStartBits;
            Palette = new()
            {
                [0] = length,
            };
            Container = null;
        }

        public int this[int index]
        {
            get
            {
                if (Container is null) return Palette.First().Key;
                return Palette[Container[index]];
            }
            set
            {
                int paletteIndex = Palette.IndexOf(value);
                if (paletteIndex >= 0)
                {
                    if (Container is null) return;
                    int oldPaletteIndex = Container[index];
                    if (oldPaletteIndex == paletteIndex) return;
                    Palette[paletteIndex]++;
                    int oldPaletteCount = Palette[oldPaletteIndex] - 1;
                    Palette[oldPaletteIndex] = oldPaletteCount;
                    if (oldPaletteCount <= 0)
                    {
                        Palette.Remove(oldPaletteIndex);
                        for (int i = 0; i < Length; i++)
                        {
                            int dataPaletteIndex = Container[i];
                            if (dataPaletteIndex > oldPaletteCount)
                            {
                                Container[i] = dataPaletteIndex - 1;
                            }
                        }
                        int bits = int.Log2(Palette.Count - 1) + 1;
                        if (bits != Container.Bits)
                        {
                            if (bits == 0)
                            {
                                Container = null;
                            }
                            if (bits > IndirectEndBits)
                            {
                                //convert to direct
                            }
                            //todo: check if bits per entry changerd, downsize
                            //todo: check if there is only 1 entry, then use single mapping if it is the case
                        }
                    }
                    Container[index] = paletteIndex;
                }
                else
                {
                    //todo: check if bits per entry would change, upsize if necessary
                    //todo: add palette entry
                    //todo: write container
                }
            }
        }
    }*/
}