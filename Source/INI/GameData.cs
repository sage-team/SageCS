using OpenTK;
using SageCS.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SageCS.INI
{
    class WeaponBonus
    {
        public string type;
        public int value;

        public WeaponBonus(string _type, int _value)
        {
            this.type = _type;
            this.value = _value;
        }
    }

    class GameData
    {
        public bool CheckMemoryLeaks;
        public string MapName;
        public string ShellMapName; // map file
        public string MoveHintName;
        public bool ShowProps;
        public bool UseFPSLimit;
        public int FramesPerSecondLimit;
        public bool Windowed;
        public int XResolution;
        public int YResolution;
        public int MaxShellScreens;
        public bool UseCloudMap;
        public bool AllowTreeFading;
        public bool UseLightMap;
        public bool AdjustCliffTextures;
        public bool StretchTerrain;
        public bool UseHalfHeightMap;
        public bool ShowObjectHealth;
        public bool HideGarrisonFlags;
        public int Use3WayTerrainBlends;
        public bool DrawEntireTerrain;
        public string TerrainLOD;
        public int TerrainLODTargetTimeMS;
        public bool RightMouseAlwaysScrolls;
        public bool UseCloudPlane;
        public bool UseShadowVolumes;
        public bool UseShadowDecals;
        public bool UseShadowMapping;
        public bool ShowSelectedUnitMaker;
        public bool UseSimpleHordeDecals;
        public bool UseSimpleMergeDecals;
        public int OpacityOfSimpleMergeDecals;
        public bool UseBehindBuildingMaker;
        public int DefaultOcclusionDelay;
        public float OccludedColorLuminanceScale;
        public float WaterPositionX;
        public float WaterPositionY;
        public float WaterPositionZ;
        public float WaterExtentX;
        public float WaterExtentY;
        public int WaterType;
        public bool ShowSelectedUnitMarker;
        public bool VTune;
        public int MaxDebugCashValueMapValue;
        public int DebugCashValueMapTileDuration;
        public int MaxDebugThreatMapValue;
        public int DebugThreatMapTileDuration;
        public Vector3 DebugVisibilityTileGapColor;
        public Vector3 DebugVisibilityTileDeshroudColor;


        public bool UseBehindBuildingMarker;
        public float UnitDamagedThreshold;
        public float TerrainResourceCellSize;
        public string AutoFireParticleMediumPrefix;
        public bool Wireframe;
        public bool StateMachineDebug;
        public bool UseCameraConstraints;
        public bool ShroudOn;
        public bool FogOfWarOn;
        public bool ShowCollisionExtends;

        public int DebugProjectileTileWidth;

        public string DefaultUnitHealingBuffFxList;
        public string DefaultStructureRepairBuffFxList;

        public float DefaultStructureRubbleHeight;
        public bool ShowCollisionExtents;
        public int DebugProjectileTileDuration;
        public Vector3 DebugProjectileTileColor;
        public int DebugAerialTileWidth;
        public int DebugAerialTileDuration;
        public Vector3 DebugAerialTileColor;
        public int DebugVisibilityTileCount;
        public float DebugVisibilityTileWidth;

        public int DebugVisibilityTileDuration;
        public Vector3 DebugVisibilityTileTargettableColor;

        public string VertexWaterAvailableMaps1;
        public float VertexWaterHeightClampLow1;
        public float VertexWaterHeightClampHi1;
        public int VertexWaterAngle1;
        public float VertexWaterXPosition1;
        public float VertexWaterYPosition1;
        public float VertexWaterZPosition1;
        public int VertexWaterXGridCells1;
        public int VertexWaterYGridCells1;
        public float VertexWaterGridSize1;
        public float VertexWaterAttenuationA1;
        public float VertexWaterAttenuationB1;
        public float VertexWaterAttenuationC1;
        public float VertexWaterAttenuationRange1;

        public string VertexWaterAvailableMaps2;
        public float VertexWaterHeightClampLow2;
        public float VertexWaterHeightClampHi2;
        public int VertexWaterAngle2;
        public float VertexWaterXPosition2;
        public float VertexWaterYPosition2;
        public float VertexWaterZPosition2;
        public int VertexWaterXGridCells2;
        public int VertexWaterYGridCells2;
        public float VertexWaterGridSize2;
        public float VertexWaterAttenuationA2;
        public float VertexWaterAttenuationB2;
        public float VertexWaterAttenuationC2;
        public float VertexWaterAttenuationRange2;

        public string VertexWaterAvailableMaps3;
        public float VertexWaterHeightClampLow3;
        public float VertexWaterHeightClampHi3;
        public int VertexWaterAngle3;
        public float VertexWaterXPosition3;
        public float VertexWaterYPosition3;
        public float VertexWaterZPosition3;
        public int VertexWaterXGridCells3;
        public int VertexWaterYGridCells3;
        public float VertexWaterGridSize3;
        public float VertexWaterAttenuationA3;
        public float VertexWaterAttenuationB3;
        public float VertexWaterAttenuationC3;
        public float VertexWaterAttenuationRange3;

        public string VertexWaterAvailableMaps4;
        public float VertexWaterHeightClampLow4;
        public float VertexWaterHeightClampHi4;
        public int VertexWaterAngle4;
        public float VertexWaterXPosition4;
        public float VertexWaterYPosition4;
        public float VertexWaterZPosition4;
        public int VertexWaterXGridCells4;
        public int VertexWaterYGridCells4;
        public float VertexWaterGridSize4;
        public float VertexWaterAttenuationA4;
        public float VertexWaterAttenuationB4;
        public float VertexWaterAttenuationC4;
        public float VertexWaterAttenuationRange4;

        public float DownwindAngle;
        public bool DrawSkyBox;

        public float DefaultCameraMinHeight;
        public float DefaultCameraMaxHeight;
        public float DefaultCameraPitchAngle;
        public float DefaultCameraYawAngle;
        public float DefaultCameraScrollSpeedScalar;

        public float CameraLockHeightDelta;
        public float CameraTerrainSampleRadiusForHeight;

        public float CameraEaseFactor;

        public float MaxCameraHeight;
        public float MinCameraHeight;

        public bool UseCameraInReplay;
        public float CameraAdjustSpeed;
        public float ScrollAmountCutoff;
        public bool EnforceMaxCameraHeight;
        public float TerrainHeightAtEdgeOfMap;
        public float UnitReallyDamagedThreshold;
        public float GroundStiffness;
        public float StructureStiffness;
        public float Gravity; // ft/sec^2

        public float PartitionCellSize;
        public float TerrainResourceCellize;

        public float ParticleScale;

        public string AutoFireParticleSmallPrefix;
        public string AutoFireParticleSmallSystem;
        public int AutoFireParticleSmallMax;
        public string AutofireParticleMediumPrefix;
        public string AutoFireParticleMediumSystem;
        public int AutoFireParticleMediumMax;
        public string AutoFireParticleLargePrefix;
        public string AutoFireParticleLargeSystem;
        public int AutoFireParticleLargeMax;
        public string AutoSmokeParticleSmallPrefix;
        public string AutoSmokeParticleSmallSystem;
        public int AutoSmokeParticleSmallMax;
        public string AutoSmokeParticleMediumPrefix;
        public string AutoSmokeParticleMediumSystem;
        public int AutoSmokeParticleMediumMax;
        public string AutoSmokeParticleLargePrefix;
        public string AutoSmokeParticleLargeSystem;
        public int AutoSmokeParticleLargeMax;
        public string AutoAflameParticlePrefix;
        public string AutoAflameParticleSystem;
        public int AutoAflameParticleMax;

        public float AmmoPipScaleFactor;
        public float ContainerPipScaleFactor;
        public Vector2 AmmoPipScreenOffset;
        public Vector2 ContainerPipScreenOffset;
        public Vector3 AmmoPipWorldOffset;
        public Vector3 ContainerPipWorldOffset;

        public string LevelGainAnimationName;
        public float LevelGainAnimationTime;
        public float LevelGainAnimationZRise;

        public string GetHealedAnimationName;
        public float GetHealedAnimationTime;
        public float GetHealedAnimationZRise;

        public string GenericDamageFieldName;
        public string GenericDamageWarningName;

        public int MaxTerrainTracks;
        public string TimeOfDay;
        public string Weather;
        public bool MakeTrackMarks;
        public bool ForceModelsToFollowTimeOfDay;
        public bool ForceModelsToFollowWeather;

        public Vector3 TerrainLightingMorningAmbient;
        public Vector3 TerrainLightingMorningDiffuse;
        public Vector3 TerrainLightingMorningLightPos;
        public Vector3 TerrainLightingEveningAmbient;
        public Vector3 TerrainLightingEveningDiffuse;
        public Vector3 TerrainLightingEveningLightPos;
        public Vector3 TerrainLightingNightAmbient;
        public Vector3 TerrainLightingNightDiffuse;
        public Vector3 TerrainLightingNightLightPos;

        public Vector3 TerrainObjectsLightingMorningAmbient;
        public Vector3 TerrainObjectsLightingMorningDiffuse;
        public Vector3 TerrainObjectsLightingMorningLightPos;
        public Vector3 TerrainObjectsLightingEveningAmbient;
        public Vector3 TerrainObjectsLightingEveningDiffuse;
        public Vector3 TerrainObjectsLightingEveningLightPos;
        public Vector3 TerrainObjectsLightingNightAmbient;
        public Vector3 TerrainObjectsLightingNightDiffuse;
        public Vector3 TerrainObjectsLightingNightLightPos;

        public Vector3 TerrainLightingAfternoonAmbient;
        public Vector3 TerrainLightingAfternoonDiffuse;
        public Vector3 TerrainLightingAfternoonLightPos;
        public Vector3 TerrainObjectsLightingAfternoonAmbient;
        public Vector3 TerrainObjectsLightingAfternoonDiffuse;
        public Vector3 TerrainObjectsLightingAfternoonLightPos;

        public Vector3 TerrainLightingAfternoonAmbient2;
        public Vector3 TerrainLightingAfternoonDiffuse2;
        public Vector3 TerrainLightingAfternoonLightPos2;
        public Vector3 TerrainObjectsLightingAfternoonAmbient2;
        public Vector3 TerrainObjectsLightingAfternoonDiffuse2;
        public Vector3 TerrainObjectsLightingAfternoonLightPos2;

        public Vector3 TerrainLightingAfternoonAmbient3;
        public Vector3 TerrainLightingAfternoonDiffuse3;
        public Vector3 TerrainLightingAfternoonLightPos3;
        public Vector3 TerrainObjectsLightingAfternoonAmbient3;
        public Vector3 TerrainObjectsLightingAfternoonDiffuse3;
        public Vector3 TerrainObjectsLightingAfternoonLightPos3;

        public bool AudioOn;
        public bool MusicOn;
        public bool SoundsOn;
        public bool SpeechOn;
        public bool VideoOn;

        public bool DebugAI;
        public bool DebugAIObstacles;

        public int MaxRoadSegments;
        public int MaxRoadVertex;
        public int MaxRoadIndex;
        public int MaxRoadTypes;

        public int GoodCommandPointLimit;
        public int EvilCommandPointLimit;
        public int PowerLimit;
        public float ResourceMultiplierLimit;

        public int InitialMaxRingLevel;

        public bool SkipMapUnroll;

        public float ResourceBonusMultiplier;

        public Vector2 GoodCommandPoints;
        public Vector2 EvilCommandPoints;

        public int GoodCommandPointsBonus;
        public int EvilCommandPointsBonus;

        public Vector2 GoodCommandPointsAI;
        public Vector2 EvilCommandPointsAI;

        public Vector2 GoodCommandPointsMP2;
        public Vector2 EvilCommandPointsMP2;

        public Vector2 GoodCommandPointsMP3;
        public Vector2 EvilCommandPointsMP3;

        public Vector2 GoodCommandPointsMP4;
        public Vector2 EvilCommandPointsMP4;

        public Vector2 GoodCommandPointsMP5;
        public Vector2 EvilCommandPointsMP5;

        public Vector2 GoodCommandPointsMP6;
        public Vector2 EvilCommandPointsMP6;

        public Vector2 GoodCommandPointsMP7;
        public Vector2 EvilCommandPointsMP7;

        public Vector2 GoodCommandPointsMP8;
        public Vector2 EvilCommandPointsMP8;

        public float[] MultiPlayMoneyMult;
        public float[] MultiPlayUnitXPMult;
        public float[] MultiPlayBuildingXPMult;

        public float[] MultiPlayUnitSpeedMult;
        public float[] MultiPlayBuildingSpeedMult;

        public int HandicapBuildSpeed5;
        public int HandicapBuildSpeed10;
        public int HandicapBuildSpeed15;
        public int HandicapBuildSpeed20;
        public int HandicapBuildSpeed25;
        public int HandicapBuildSpeed30;
        public int HandicapBuildSpeed35;
        public int HandicapBuildSpeed40;
        public int HandicapBuildSpeed45;
        public int HandicapBuildSpeed50;
        public int HandicapBuildSpeed55;
        public int HandicapBuildSpeed60;
        public int HandicapBuildSpeed65;
        public int HandicapBuildSpeed70;
        public int HandicapBuildSpeed75;
        public int HandicapBuildSpeed80;
        public int HandicapBuildSpeed85;
        public int HandicapBuildSpeed90;
        public int HandicapBuildSpeed95;
        public int HandicapBuildSpeed100;

        public int ValuePerSupplyBox;
        public int SupplyBoxesPerTree;

        public float BuildSpeed;
        public float MinDistFromEdgeOfMapForBuild;
        public float SupplyBuildBorder;

        public float AllowedHeightVariationForBuilding;

        public float MinLowEnergyProductionSpeed;
        public float MaxLowEnergyProductionSpeed;
        public float LowEnergyPenaltyModifier;
        public float MultipleFactory;
        public float RefundPercent;
        public float StealthFriendlyOpacity;

        public float CommandCenterHealRange;
        public float CommandCenterHealAmount;
        public int MaxLineBuildObjects;
        public int MaxTunnelCapacity;

        public float StandardMinefieldDensity;
        public int StandardMinefieldDistance;


        public float HorizontalScrollSpeedFactor;
        public float VerticalScrollSpeedFactor;

        public float ScreenEdgeScrollSpeedFactor;
        public float ScreenEdgeScrollRampTime;

        public float KeyboardScrollSpeedFactor;
        public string MovementPenaltyDamageState;

        public int MaxParticleCount;
        public int MaxFieldParticleCount;

        public Dictionary<string, WeaponBonus> WeaponBonus =  new Dictionary<string, WeaponBonus>();

        public int HealthBonus_Regular;
        public int HealthBonus_Veteran;
        public int HealthBonus_Elite;
        public int HealthBonus_Heroic;

        public int HumanSoloPlayerHealthBonus_Easy;
        public int HumanSoloPlayerHealthBonus_Normal;
        public int HumanSoloPlayerHealthBonus_Hard;

        public int AttributeModifierArmorMaxBonus;  
    
        public string VolumeDistribution; 
        public float VolumeMuValue;

        public int GroupSelectMinSelectSize;
        public float GroupSelectVolumeBase;
        public float GroupSelectVolumeIncrement;
        public int MaxUnitSelectSounds;

        public float DamageRadiusMinimumForSplash;

        public float SelectionFlashSaturationFactor;
        public bool SelectionFlashHouseColor;

        public int CameraAudibleRadius;
        public float GroupMoveClickToGatherAreaFactor;

        public float ShakeSubtleIntensity;
        public float ShakeNormalIntensity;
        public float ShakeStrongIntensity;
        public float ShakeSevereIntensity;
        public float ShakeCineExtremeIntensity;
        public float ShakeCineInsaneIntensity;

        public float MaxShakeIntensity;
        public float MaxShakeRange;

        public int SellPercentage;

        public float BaseRegenHealthPercentPerSecond;
        public int BaseRegenDelay;

        public string SpecialPowerViewObject;

        public List<string> StandardPublicBone = new List<string>();

        public int DefaultStartingCash;

        public int UnlookPersistDuration;

        public Vector3 ShroudColor;
        public int ClearAlpha;
        public int FogAlpha;
        public int ShroudAlpha;
        public bool TaintOn;
        public Vector3 TaintColor;
        public int TaintAlpha;
        public Vector3 ElvenWoodColor;

        public int NetworkFPSHistoryLength;
        public int NetworkLatencyHistoryLength;
        public int NetworkRunAheadMetricsTime;
        public int NetworkCushionHistoryLength;
        public int NetworkRunAheadSlack;
        public int NetworkKeepAliveDelay;
        public int NetworkDisconnectTime;
        public int NetworkPlayerTimeoutTime;
        public int NetworkDisconnectScreenNotifyTime;

        public float KeyboardCameraRotateSpeed;

        public string UserDataLeafName;

        public int DefaultVoiceAttackChargeTimeout;

        public int DefaultMaxDistanceForEngaged;
        public int DefaultEngagedStateTimeout;

        public int AnimationSharingCap;
        public int AnimationSharingFrameTolerance;
        public float AnimationSharingSpeedTolerance;
        public float AnimationSharingWorryThreshold;
        public float AnimationSharingDrasticThreshold;

        public string ParticleCursorAnim2DTemplateName;
        public int ParticleCursorBurstCount;
        public string ParticleCursorBurstFactor;
        public float ParticleCursorStopBurstFactor;
        public int ParticleCursorBurstFrequency;
        public Vector2 ParticleCursorParticleLife;
        public Vector2 ParticleCursorSystemLife;
        public Vector2 ParticleCursorDriftVelX;
        public Vector2 ParticleCursorDriftVelY;
        public Vector2 ParticleCursorVelocityDrag;
        public Vector2 ParticleCursorParticleSize;
        public bool ParticleCursorPerFrameSize;
        public int ParticleCursorAlpha;
        public Vector2 ParticleCursorOffset;

        public Vector2 ProgressMovieOffset;
        public Vector2 ProgressMovieSize;

        public bool UseHelpTextSystem;
        public bool EnableHouseColor;

        public string TreeFadeObjectFilter;
        public string CamouflageDetectorObjectFilter;
        public string VeterancyPipDrawObjectFilter;

        public int ReinvisibityDelay;
        public float InvisibilityOpacityMin;
        public int InvisibilityOpacityMax;
        public int InvisibilityOpacityCycleFrames;

        public int BuilderFadeOutTime;
        public int BuilderFadeInTime;
        public int BuilderMoveFromNewStructureDistance;
        public int MaxCastleRadius;

        public string VictoryConditionStructureObjectFilter;

        public string VictoryConditionUnitObjectFilter;

        public string BasicTutorialMap;
        public string BasicTutorialLoadMovie;
        public string BasicTutorialLoadScreenStillImage;
        public string BasicTutorialLoadScreenVoiceTrack;
        public string BasicTutorialLoadScreenMusicTrack;
        public string BasicTutorialObjective;
        public int BasicTutorialMillisecondsAfterStartToStartFadeUp;

        public string AdvancedTutorialMap;
        public string AdvancedTutorialLoadMovie;
        public string AdvancedTutorialLoadScreenStillImage;
        public string AdvancedTutorialLoadScreenVoiceTrack;
        public string AdvancedTutorialLoadScreenMusicTrack;
        public string AdvancedTutorialObjective;
        public int AdvancedTutorialMillisecondsAfterStartToStartFadeUp;

        public string ObjectsThatScore;
        public int ScoreKeeper_UnitsBuiltMultiplier;
        public int ScoreKeeper_UnitsDestroyedMultiplier;
        public int ScoreKeeper_StructuresBuiltMultiplier;
        public int ScoreKeeper_StructuresDestroyedMultiplier;
        public int ScoreKeeper_HeroesVettedMultiplier;
        public int ScoreKeeper_UnitsVettedMultiplier;
        public int ScoreKeeper_ObjectivesCompletedMultiplier;
        public int ScoreKeeper_SuppliesCollectedMultiplier;
        public int ScoreKeeper_SkillPointsMultiplier;
        public int ScoreKeeper_PowerPointsMultiplier;
        public int ScoreKeeper_RegionCommandPointsMultiplier;
        public int ScoreKeeper_RegionResourcesMultiplier;
        public int ScoreKeeper_RegionPowerPointsMultiplier;
        public int ScoreKeeper_TimeTakenMultiplier;
        public int ScoreKeeper_TimeTakenMaximumScore;
        public int ScoreKeeper_TimeTakenMinimumScore;
        public int ScoreKeeper_TotalVictoryRequiredScore;
        public int ScoreKeeper_NormalVictoryRequiredScore;
        public int ScoreKeeper_NormalVictoryRequiredObjectivesPercentage;
        public float ScoreKeeper_PlayerEliminatedMultiplier;

        public int TintUnitIfPathingForMoreThan;
        public float GarrisonedRangeMultiplier;

        public int MaxPathfindCellsPerFrame;
        public int MaxCellsFindMeleeEngagementLocation;
        public int MaxCellsAdjustDestination;
        public int MaxCellsAdjustHordeMeleeDestination;
        public int MaxCellsAdjustTargetDestination;
        public int MaxCellsAdjustToPossibleDestination;
        public int MaxCellsAdjustToMeleeDestination;
        public int MaxCellsAdjustToNearestGroundCell;
        public int MaxCellsAdjustToNearestValidCell;
        public int MaxCellsPatchPath;
        public int MaxCellsFindPathLimit;
        public int MaxCellsFindAttackPath;
        public int MaxCellsFindAttackPathSideways;
        public int MaxCellsToExamineTowardsGoal;

        public int NumMinutesBeforePlayersCanTransferMoney;

        public int MaxNumMembersToForceToImmediatelyEnter;
        public int WaitToForceMemberToEnterDelay;
    }
}
