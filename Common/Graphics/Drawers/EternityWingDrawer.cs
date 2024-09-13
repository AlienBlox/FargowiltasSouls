// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Common.Graphics.Drawers.EternityWingDrawer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.PlayerDrawLayers;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.Graphics.Renderers;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Common.Graphics.Drawers
{
  public class EternityWingDrawer : ModSystem
  {
    public static ManagedRenderTarget WingTarget { get; private set; }

    private static bool WingsInUse { get; set; }

    public static bool DoNotDrawSpecialWings { get; private set; }

    public virtual void Load()
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      EternityWingDrawer.WingTarget = new ManagedRenderTarget(true, EternityWingDrawer.\u003C\u003EO.\u003C0\u003E__CreateScreenSizedTarget ?? (EternityWingDrawer.\u003C\u003EO.\u003C0\u003E__CreateScreenSizedTarget = new ManagedRenderTarget.RenderTargetInitializationAction((object) null, __methodptr(CreateScreenSizedTarget))), true);
      Main.OnPreDraw += new Action<GameTime>(this.PrepareWingTarget);
      // ISSUE: method pointer
      On_LegacyPlayerRenderer.DrawPlayers += new On_LegacyPlayerRenderer.hook_DrawPlayers((object) this, __methodptr(DrawWingTarget));
      // ISSUE: method pointer
      On_PlayerDrawLayers.DrawPlayer_09_Wings += new On_PlayerDrawLayers.hook_DrawPlayer_09_Wings((object) this, __methodptr(PreventDefaultWingDrawing));
      // ISSUE: method pointer
      On_Player.WingFrame += new On_Player.hook_WingFrame((object) this, __methodptr(ManuallyAnimateCustomWingsIHateThis));
    }

    private void ManuallyAnimateCustomWingsIHateThis(
      On_Player.orig_WingFrame orig,
      Player self,
      bool wingFlap,
      bool isCustomWings)
    {
      if (self.wings == EternitySoul.WingSlotID)
      {
        if (wingFlap || self.jump > 0)
        {
          int num1 = 4;
          int num2 = 5;
          ++self.wingFrameCounter;
          if (self.wingFrameCounter <= num1)
            return;
          ++self.wingFrame;
          self.wingFrameCounter = 0;
          if (self.wingFrame < num2)
            return;
          self.wingFrame = 0;
        }
        else
          self.wingFrame = 0;
      }
      else
        orig.Invoke(self, wingFlap, isCustomWings);
    }

    public virtual void Unload()
    {
      Main.OnPreDraw -= new Action<GameTime>(this.PrepareWingTarget);
      // ISSUE: method pointer
      On_LegacyPlayerRenderer.DrawPlayers -= new On_LegacyPlayerRenderer.hook_DrawPlayers((object) this, __methodptr(DrawWingTarget));
      // ISSUE: method pointer
      On_PlayerDrawLayers.DrawPlayer_09_Wings -= new On_PlayerDrawLayers.hook_DrawPlayer_09_Wings((object) this, __methodptr(PreventDefaultWingDrawing));
      // ISSUE: method pointer
      On_Player.WingFrame -= new On_Player.hook_WingFrame((object) this, __methodptr(ManuallyAnimateCustomWingsIHateThis));
    }

    private void PrepareWingTarget(GameTime obj)
    {
      EternityWingDrawer.WingsInUse = false;
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        Player player = Main.player[index];
        if (player.wings == EternitySoul.WingSlotID && ((Entity) player).active && !player.dead)
        {
          EternityWingDrawer.WingsInUse = true;
          break;
        }
      }
      if (!ShaderManager.HasFinishedLoading || Main.gameMenu || !EternityWingDrawer.WingsInUse)
        return;
      EternityWingDrawer.WingTarget.SwapToRenderTarget(new Color?());
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.EternitySoulWings");
      FargosTextureRegistry.TurbulentNoise.Value.SetTexture1();
      FargosTextureRegistry.ColorNoiseMap.Value.SetTexture2();
      shader.TrySetParameter("lightning", (object) Color.White);
      shader.TrySetParameter("resolution", (object) Vector2.op_Division(Utils.Size((Texture2D) EternityWingDrawer.WingTarget.Target), 2f));
      shader.Apply("AutoloadPass");
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, Main.Rasterizer, shader.Shader.Value, Matrix.Identity);
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        Player player = Main.player[index];
        if (player.wings == EternitySoul.WingSlotID && !player.dead && ((Entity) player).active)
        {
          PlayerDrawSet drawinfo = new PlayerDrawSet();
          ((PlayerDrawSet) ref drawinfo).BoringSetup(player, new List<DrawData>(), new List<int>(), new List<int>(), Vector2.op_Addition(((Entity) player).TopLeft, Vector2.op_Multiply(Vector2.UnitY, player.gfxOffY)), 0.0f, player.fullRotation, player.fullRotationOrigin);
          EternityWingDrawer.DoNotDrawSpecialWings = false;
          EternitySoulWingPlayerLayer.DrawWings(ref drawinfo);
          EternityWingDrawer.DoNotDrawSpecialWings = true;
          foreach (DrawData drawData in drawinfo.DrawDataCache)
            ((DrawData) ref drawData).Draw(Main.spriteBatch);
        }
      }
      Main.spriteBatch.End();
      ((Game) Main.instance).GraphicsDevice.SetRenderTarget((RenderTarget2D) null);
    }

    private void DrawWingTarget(
      On_LegacyPlayerRenderer.orig_DrawPlayers orig,
      LegacyPlayerRenderer self,
      Camera camera,
      IEnumerable<Player> players)
    {
      if (EternityWingDrawer.WingsInUse)
      {
        Main.spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, camera.Rasterizer, (Effect) null, camera.GameViewMatrix.TransformationMatrix);
        if (Main.LocalPlayer.cWings != 0)
          GameShaders.Armor.Apply(Main.LocalPlayer.cWings, (Entity) Main.LocalPlayer, new DrawData?());
        Main.spriteBatch.Draw((Texture2D) ManagedRenderTarget.op_Implicit(EternityWingDrawer.WingTarget), Vector2.op_Subtraction(Main.screenLastPosition, Main.screenPosition), Color.White);
        Main.spriteBatch.End();
      }
      orig.Invoke(self, camera, players);
    }

    private void PreventDefaultWingDrawing(
      On_PlayerDrawLayers.orig_DrawPlayer_09_Wings orig,
      ref PlayerDrawSet drawinfo)
    {
      if (drawinfo.hideEntirePlayer || drawinfo.drawPlayer.dead || drawinfo.drawPlayer.wings == EternitySoul.WingSlotID)
        return;
      orig.Invoke(ref drawinfo);
    }
  }
}
