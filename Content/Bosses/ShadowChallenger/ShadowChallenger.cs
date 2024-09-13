// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Luminance.Common.StateMachines;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.ShadowChallenger
{
  public class ShadowChallenger : ModNPC
  {
    private PushdownAutomata<EntityAIState<FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>, FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates> stateMachine;
    public const int FightIntro_AttackLength = 120;
    public const int FogCharges_AttackLength = 360;
    public const int FogTears_AttackLength = 240;

    public virtual bool IsLoadingEnabled(Mod mod) => false;

    public ref float CurrentPhase => ref this.NPC.ai[0];

    public float Timer
    {
      get => (float) this.StateMachine.CurrentState.Time;
      set => this.StateMachine.CurrentState.Time = (int) value;
    }

    public ref float AI1 => ref this.NPC.ai[1];

    public ref float AI2 => ref this.NPC.ai[2];

    public ref float AI3 => ref this.NPC.ai[3];

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 50;
      ((Entity) this.NPC).height = 50;
      this.NPC.lifeMax = 50000;
    }

    public virtual void AI()
    {
      if (this.StateMachine?.StateStack?.Count.GetValueOrDefault(1) <= 0)
        this.StateMachine.StateStack.Push(this.StateMachine.StateRegistry[FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates.RefillStates]);
      this.StateMachine.PerformBehaviors();
      this.StateMachine.PerformStateTransitionCheck();
      if (this.StateMachine.StateStack.Count <= 0)
        return;
      ++this.Timer;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.StateMachine.StateStack.Count);
      EntityAIState<FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>[] array = this.StateMachine.StateStack.ToArray();
      for (int index = 0; index < this.StateMachine.StateStack.Count; ++index)
      {
        writer.Write((int) array[index].Identifier);
        writer.Write(array[index].Time);
      }
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      int num = reader.ReadInt32();
      this.StateMachine.StateStack.Clear();
      for (int index = 0; index < num; ++index)
      {
        EntityAIState<FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates> entityAiState = this.StateMachine.StateRegistry[(FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates) reader.ReadInt32()];
        entityAiState.Time = reader.ReadInt32();
        this.StateMachine.StateStack.Push(entityAiState);
      }
    }

    public PushdownAutomata<EntityAIState<FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>, FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates> StateMachine
    {
      get
      {
        if (this.stateMachine == null)
          this.SetupStateMachine();
        return this.stateMachine;
      }
      private set => this.stateMachine = value;
    }

    private void SetupStateMachine()
    {
      this.StateMachine = new PushdownAutomata<EntityAIState<FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>, FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>(new EntityAIState<FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>(FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates.FightIntro));
      for (int index = 0; index < 4; ++index)
        this.StateMachine.RegisterState(new EntityAIState<FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>((FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates) index));
      // ISSUE: method pointer
      this.StateMachine.OnStateTransition += new PushdownAutomata<EntityAIState<FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>, FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>.OnStateTransitionDelegate((object) this, __methodptr(OnAnyStateTransition));
      AutoloadAsBehavior<EntityAIState<FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>, FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>.FillStateMachineBehaviors<FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger>(this.StateMachine, this);
      AutomatedMethodInvokeAttribute.InvokeWithAttribute((object) this);
    }

    private void OnAnyStateTransition(
      bool stateWasPopped,
      EntityAIState<FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates> oldState)
    {
      this.NPC.netUpdate = true;
      this.NPC.TargetClosest(false);
      if (!stateWasPopped)
        return;
      this.AI1 = 0.0f;
      this.AI2 = 0.0f;
      this.AI3 = 0.0f;
    }

    [AutomatedMethodInvoke]
    public void LoadTransition_RefillAttacks()
    {
      this.StateMachine.RegisterTransition(FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates.RefillStates, new FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates?(), false, (Func<bool>) (() => true), (Action) (() => { }));
    }

    [AutomatedMethodInvoke]
    public void LoadTransitions_FightIntro()
    {
      this.StateMachine.RegisterTransition(FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates.FightIntro, new FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates?(FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates.FogCharges), false, (Func<bool>) (() => (double) this.Timer > 120.0), (Action) null);
    }

    [AutoloadAsBehavior<EntityAIState<FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>, FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>(FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates.FightIntro)]
    public void DoBehavior_FightIntro()
    {
    }

    [AutomatedMethodInvoke]
    public void LoadTransition_FogCharges()
    {
      this.StateMachine.RegisterTransition(FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates.FogCharges, new FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates?(FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates.FogTears), false, (Func<bool>) (() => (double) this.Timer > 360.0), (Action) null);
    }

    [AutoloadAsBehavior<EntityAIState<FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>, FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>(FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates.FogCharges)]
    public void DoBehavior_FogCharges()
    {
    }

    [AutomatedMethodInvoke]
    public void LoadTransition_FogTears()
    {
      this.StateMachine.RegisterTransition(FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates.FogTears, new FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates?(), false, (Func<bool>) (() => (double) this.Timer > 240.0), (Action) null);
    }

    [AutoloadAsBehavior<EntityAIState<FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>, FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates>(FargowiltasSouls.Content.Bosses.ShadowChallenger.ShadowChallenger.BehaviorStates.FogTears)]
    public void DoBehavior_FogTears()
    {
    }

    public enum BehaviorStates
    {
      FightIntro,
      FogCharges,
      FogTears,
      RefillStates,
      Count,
    }
  }
}
