// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.BossBags.BossBag
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Items.BossBags
{
  public abstract class BossBag : SoulsItem
  {
    protected abstract bool IsPreHMBag { get; }

    public virtual void SetStaticDefaults()
    {
      ItemID.Sets.BossBag[this.Item.type] = true;
      if (this.IsPreHMBag)
        ItemID.Sets.PreHardmodeLikeBossBag[this.Item.type] = true;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 3;
    }

    public virtual void SetDefaults()
    {
      this.Item.maxStack = 999;
      this.Item.consumable = true;
      ((Entity) this.Item).width = 24;
      ((Entity) this.Item).height = 24;
      this.Item.rare = 11;
      this.Item.expert = true;
    }

    public virtual bool CanRightClick() => true;

    public virtual bool PreDrawInWorld(
      SpriteBatch spriteBatch,
      Color lightColor,
      Color alphaColor,
      ref float rotation,
      ref float scale,
      int whoAmI)
    {
      Texture2D texture2D = TextureAssets.Item[this.Item.type].Value;
      Rectangle rectangle = Main.itemAnimations[this.Item.type] == null ? Utils.Frame(texture2D, 1, 1, 0, 0, 0, 0) : Main.itemAnimations[this.Item.type].GetFrame(texture2D, Main.itemFrameCounter[whoAmI]);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Vector2 vector2_2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_2).\u002Ector((float) (((Entity) this.Item).width / 2) - vector2_1.X, (float) (((Entity) this.Item).height - rectangle.Height));
      Vector2 vector2_3 = Vector2.op_Addition(Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Item).position, Main.screenPosition), vector2_1), vector2_2);
      float timeWrappedHourly = Main.GlobalTimeWrappedHourly;
      float num1 = (float) ((double) this.Item.timeSinceItemSpawned / 240.0 + (double) timeWrappedHourly * 0.039999999105930328);
      float num2 = timeWrappedHourly % 4f / 2f;
      if ((double) num2 >= 1.0)
        num2 = 2f - num2;
      float num3 = (float) ((double) num2 * 0.5 + 0.5);
      for (float num4 = 0.0f; (double) num4 < 1.0; num4 += 0.25f)
      {
        float num5 = (float) (((double) num4 + (double) num1) * 6.2831854820251465);
        spriteBatch.Draw(texture2D, Vector2.op_Addition(vector2_3, Vector2.op_Multiply(Utils.RotatedBy(new Vector2(0.0f, 8f), (double) num5, new Vector2()), num3)), new Rectangle?(rectangle), new Color(90, 70, (int) byte.MaxValue, 50), rotation, vector2_1, scale, (SpriteEffects) 0, 0.0f);
      }
      for (float num6 = 0.0f; (double) num6 < 1.0; num6 += 0.34f)
      {
        float num7 = (float) (((double) num6 + (double) num1) * 6.2831854820251465);
        spriteBatch.Draw(texture2D, Vector2.op_Addition(vector2_3, Vector2.op_Multiply(Utils.RotatedBy(new Vector2(0.0f, 4f), (double) num7, new Vector2()), num3)), new Rectangle?(rectangle), new Color(140, 120, (int) byte.MaxValue, 77), rotation, vector2_1, scale, (SpriteEffects) 0, 0.0f);
      }
      return true;
    }
  }
}
