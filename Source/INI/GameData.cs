using OpenTK;
using System.Collections.Generic;

namespace SageCS.INI
{
    class GameData
    {
        public bool AdjustCliffTextures;
        public string AdvancedTutorialLoadMovie;
        public string AdvancedTutorialLoadScreenMusicTrack;
        public string AdvancedTutorialLoadScreenStillImage;
        public string AdvancedTutorialLoadScreenVoiceTrack;
        public string AdvancedTutorialMap;
        public int AdvancedTutorialMillisecondsAfterStartToStartFadeUp;
        public string AdvancedTutorialObjective;
        public float AllowedHeightVariationForBuilding;
        public bool AllowTreeFading;
        public float AmmoPipScaleFactor;
        public Vector2 AmmoPipScreenOffset;
        public Vector3 AmmoPipWorldOffset;
        public int AnimationSharingCap;
        public float AnimationSharingDrasticThreshold;
        public int AnimationSharingFrameTolerance;
        public float AnimationSharingSpeedTolerance;
        public float AnimationSharingWorryThreshold;
        public int AttributeModifierArmorMaxBonus;
        public bool AudioOn;
        public int AutoAflameParticleMax;
        public string AutoAflameParticlePrefix;
        public string AutoAflameParticleSystem;
        public int AutoFireParticleLargeMax;
        public string AutoFireParticleLargePrefix;
        public string AutoFireParticleLargeSystem;
        public int AutoFireParticleMediumMax;
        public string AutoFireParticleMediumPrefix;
        public string AutoFireParticleMediumSystem;
        public int AutoFireParticleSmallMax;
        public string AutoFireParticleSmallPrefix;
        public string AutoFireParticleSmallSystem;
        public int AutoSmokeParticleLargeMax;
        public string AutoSmokeParticleLargePrefix;
        public string AutoSmokeParticleLargeSystem;
        public int AutoSmokeParticleMediumMax;
        public string AutoSmokeParticleMediumPrefix;
        public string AutoSmokeParticleMediumSystem;
        public int AutoSmokeParticleSmallMax;
        public string AutoSmokeParticleSmallPrefix;
        public string AutoSmokeParticleSmallSystem;

        public int BaseRegenDelay;
        public float BaseRegenHealthPercentPerSecond;
        public string BasicTutorialLoadMovie;
        public string BasicTutorialLoadScreenMusicTrack;
        public string BasicTutorialLoadScreenStillImage;
        public string BasicTutorialLoadScreenVoiceTrack;
        public string BasicTutorialMap;
        public int BasicTutorialMillisecondsAfterStartToStartFadeUp;
        public string BasicTutorialObjective;
        public int BuilderFadeInTime;
        public int BuilderFadeOutTime;
        public int BuilderMoveFromNewStructureDistance;
        public float BuildSpeed;

        public float CameraAdjustSpeed;
        public int CameraAudibleRadius;
        public float CameraEaseFactor;
        public float CameraLockHeightDelta;
        public float CameraTerrainSampleRadiusForHeight;
        public string CamouflageDetectorObjectFilter;
        public bool CheckMemoryLeaks;
        public int ClearAlpha;
        public float CommandCenterHealAmount;
        public float CommandCenterHealRange;
        public float ContainerPipScaleFactor;
        public Vector2 ContainerPipScreenOffset;
        public Vector3 ContainerPipWorldOffset;

        public float DamageRadiusMinimumForSplash;
        public Vector3 DebugAerialTileColor;
        public int DebugAerialTileDuration;
        public int DebugAerialTileWidth;
        public bool DebugAI;
        public bool DebugAIObstacles;
        public int DebugCashValueMapTileDuration;
        public Vector3 DebugProjectileTileColor;
        public int DebugProjectileTileDuration;
        public int DebugProjectileTileWidth;
        public int DebugVisibilityTileCount;
        public int DebugVisibilityTileDuration;
        public Vector3 DebugVisibilityTileTargettableColor;
        public float DebugVisibilityTileWidth;
        public float DefaultCameraMaxHeight;
        public float DefaultCameraMinHeight;
        public float DefaultCameraPitchAngle;
        public float DefaultCameraScrollSpeedScalar;
        public float DefaultCameraYawAngle;
        public int DefaultEngagedStateTimeout;
        public int DefaultMaxDistanceForEngaged;
        public string DefaultStructureRepairBuffFxList;
        public float DefaultStructureRubbleHeight;
        public int DebugThreatMapTileDuration;
        public Vector3 DebugVisibilityTileDeshroudColor;
        public Vector3 DebugVisibilityTileGapColor;
        public int DefaultVoiceAttackChargeTimeout;
        public int DefaultOcclusionDelay;
        public int DefaultStartingCash;
        public string DefaultUnitHealingBuffFxList;
        public float DownwindAngle;
        public bool DrawEntireTerrain;
        public bool DrawSkyBox;

        public Vector3 ElvenWoodColor;
        public bool EnableHouseColor;
        public bool EnforceMaxCameraHeight;
        public int EvilCommandPointLimit;
        public Vector2 EvilCommandPoints;
        public Vector2 EvilCommandPointsAI;
        public int EvilCommandPointsBonus;
        public Vector2 EvilCommandPointsMP2;
        public Vector2 EvilCommandPointsMP3;
        public Vector2 EvilCommandPointsMP4;
        public Vector2 EvilCommandPointsMP5;
        public Vector2 EvilCommandPointsMP6;
        public Vector2 EvilCommandPointsMP7;
        public Vector2 EvilCommandPointsMP8;

        public int FogAlpha;
        public bool FogOfWarOn;
        public bool ForceModelsToFollowTimeOfDay;
        public bool ForceModelsToFollowWeather;
        public int FramesPerSecondLimit;

        public float GarrisonedRangeMultiplier;
        public string GenericDamageFieldName;
        public string GenericDamageWarningName;
        public string GetHealedAnimationName;
        public float GetHealedAnimationTime;
        public float GetHealedAnimationZRise;
        public int GoodCommandPointLimit;
        public Vector2 GoodCommandPoints;
        public Vector2 GoodCommandPointsAI;
        public int GoodCommandPointsBonus;
        public Vector2 GoodCommandPointsMP2;
        public Vector2 GoodCommandPointsMP3;
        public Vector2 GoodCommandPointsMP4;
        public Vector2 GoodCommandPointsMP5;
        public Vector2 GoodCommandPointsMP6;
        public Vector2 GoodCommandPointsMP7;
        public Vector2 GoodCommandPointsMP8;
        public float Gravity; // ft/sec^2
        public float GroupMoveClickToGatherAreaFactor;
        public int GroupSelectMinSelectSize;
        public float GroupSelectVolumeBase;
        public float GroupSelectVolumeIncrement;
        public float GroundStiffness;

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
        public int HealthBonus_Elite;
        public int HealthBonus_Heroic;
        public int HealthBonus_Regular;
        public int HealthBonus_Veteran;
        public bool HideGarrisonFlags;
        public float HorizontalScrollSpeedFactor;
        public int HumanSoloPlayerHealthBonus_Easy;
        public int HumanSoloPlayerHealthBonus_Hard;
        public int HumanSoloPlayerHealthBonus_Normal;

        public int InitialMaxRingLevel;
        public int InvisibilityOpacityCycleFrames;
        public int InvisibilityOpacityMax;
        public float InvisibilityOpacityMin;

        public float KeyboardCameraRotateSpeed;
        public float KeyboardScrollSpeedFactor;

        public string LevelGainAnimationName;
        public float LevelGainAnimationTime;
        public float LevelGainAnimationZRise;
        public float LowEnergyPenaltyModifier;

        public bool MakeTrackMarks;
        public string MapName;
        public float MaxCameraHeight;
        public int MaxCastleRadius;
        public int MaxCellsAdjustDestination;
        public int MaxCellsAdjustHordeMeleeDestination;
        public int MaxCellsAdjustTargetDestination;
        public int MaxCellsAdjustToMeleeDestination;
        public int MaxCellsAdjustToNearestGroundCell;
        public int MaxCellsAdjustToNearestValidCell;
        public int MaxCellsAdjustToPossibleDestination;
        public int MaxCellsFindAttackPath;
        public int MaxCellsFindAttackPathSideways;
        public int MaxCellsFindMeleeEngagementLocation;
        public int MaxCellsFindPathLimit;
        public int MaxCellsPatchPath;
        public int MaxCellsToExamineTowardsGoal;
        public int MaxDebugCashValueMapValue;
        public int MaxDebugThreatMapValue;
        public int MaxFieldParticleCount;
        public int MaxNumMembersToForceToImmediatelyEnter;
        public int MaxLineBuildObjects;
        public float MaxLowEnergyProductionSpeed;
        public int MaxParticleCount;
        public int MaxPathfindCellsPerFrame;
        public int MaxRoadIndex;
        public int MaxRoadSegments;
        public int MaxRoadTypes;
        public int MaxRoadVertex;
        public float MaxShakeIntensity;
        public float MaxShakeRange;
        public int MaxShellScreens;
        public int MaxTerrainTracks;
        public int MaxTunnelCapacity;
        public int MaxUnitSelectSounds;
        public float MinCameraHeight;
        public float MinDistFromEdgeOfMapForBuild;
        public float MinLowEnergyProductionSpeed;
        public string MoveHintName;
        public string MovementPenaltyDamageState;
        public float[] MultiPlayBuildingSpeedMult;
        public float[] MultiPlayBuildingXPMult;
        public float[] MultiPlayMoneyMult;
        public float[] MultiPlayUnitSpeedMult;
        public float[] MultiPlayUnitXPMult;
        public float MultipleFactory;
        public bool MusicOn;

        public int NetworkCushionHistoryLength;
        public int NetworkDisconnectScreenNotifyTime;
        public int NetworkDisconnectTime;
        public int NetworkFPSHistoryLength;
        public int NetworkKeepAliveDelay;
        public int NetworkLatencyHistoryLength;
        public int NetworkPlayerTimeoutTime;
        public int NetworkRunAheadMetricsTime;
        public int NetworkRunAheadSlack;
        public int NumMinutesBeforePlayersCanTransferMoney;

        public string ObjectsThatScore;
        public float OccludedColorLuminanceScale;
        public int OpacityOfSimpleMergeDecals;

        public int ParticleCursorAlpha;
        public string ParticleCursorAnim2DTemplateName;
        public int ParticleCursorBurstCount;
        public string ParticleCursorBurstFactor;
        public int ParticleCursorBurstFrequency;
        public Vector2 ParticleCursorDriftVelX;
        public Vector2 ParticleCursorDriftVelY;
        public Vector2 ParticleCursorOffset;
        public Vector2 ParticleCursorParticleLife;
        public Vector2 ParticleCursorParticleSize;
        public bool ParticleCursorPerFrameSize;
        public float ParticleCursorStopBurstFactor;
        public Vector2 ParticleCursorSystemLife;
        public Vector2 ParticleCursorVelocityDrag;
        public float ParticleScale;
        public float PartitionCellSize;
        public int PowerLimit;
        public Vector2 ProgressMovieOffset;
        public Vector2 ProgressMovieSize;

        public float RefundPercent;
        public int ReinvisibityDelay;
        public float ResourceBonusMultiplier;
        public float ResourceMultiplierLimit;
        public bool RightMouseAlwaysScrolls;

        public int ScoreKeeper_HeroesVettedMultiplier;
        public int ScoreKeeper_NormalVictoryRequiredObjectivesPercentage;
        public int ScoreKeeper_NormalVictoryRequiredScore;
        public int ScoreKeeper_ObjectivesCompletedMultiplier;
        public float ScoreKeeper_PlayerEliminatedMultiplier;
        public int ScoreKeeper_PowerPointsMultiplier;
        public int ScoreKeeper_RegionCommandPointsMultiplier;
        public int ScoreKeeper_RegionPowerPointsMultiplier;
        public int ScoreKeeper_RegionResourcesMultiplier;
        public int ScoreKeeper_SkillPointsMultiplier;
        public int ScoreKeeper_StructuresBuiltMultiplier;
        public int ScoreKeeper_StructuresDestroyedMultiplier;
        public int ScoreKeeper_SuppliesCollectedMultiplier;
        public int ScoreKeeper_TimeTakenMaximumScore;
        public int ScoreKeeper_TimeTakenMinimumScore;
        public int ScoreKeeper_TimeTakenMultiplier;
        public int ScoreKeeper_TotalVictoryRequiredScore;
        public int ScoreKeeper_UnitsBuiltMultiplier;
        public int ScoreKeeper_UnitsDestroyedMultiplier;
        public int ScoreKeeper_UnitsVettedMultiplier;
        public float ScreenEdgeScrollRampTime;
        public float ScreenEdgeScrollSpeedFactor;
        public float ScrollAmountCutoff;
        public bool SelectionFlashHouseColor;
        public float SelectionFlashSaturationFactor;
        public int SellPercentage;
        public float ShakeCineExtremeIntensity;
        public float ShakeCineInsaneIntensity;
        public float ShakeNormalIntensity;
        public float ShakeSevereIntensity;
        public float ShakeStrongIntensity;
        public float ShakeSubtleIntensity;
        public string ShellMapName; // map file
        public bool ShowCollisionExtends;
        public bool ShowObjectHealth;
        public bool ShowProps;
        public bool ShowSelectedUnitMarker;
        public int ShroudAlpha;
        public Vector3 ShroudColor;
        public bool ShroudOn;
        public bool SkipMapUnroll;
        public bool SoundsOn;
        public bool SpeechOn;
        public string SpecialPowerViewObject;
        public float StandardMinefieldDensity;
        public int StandardMinefieldDistance;
        public List<string> standardPublicBones = new List<string>();
        public bool StateMachineDebug;
        public float StealthFriendlyOpacity;
        public bool StretchTerrain;
        public float StructureStiffness;
        public int SupplyBoxesPerTree;
        public float SupplyBuildBorder;

        public int TaintAlpha;
        public Vector3 TaintColor;
        public bool TaintOn;
        public float TerrainHeightAtEdgeOfMap;
        public Vector3 TerrainLightingAfternoonAmbient;
        public Vector3 TerrainLightingAfternoonAmbient2;
        public Vector3 TerrainLightingAfternoonAmbient3;
        public Vector3 TerrainLightingAfternoonDiffuse;
        public Vector3 TerrainLightingAfternoonDiffuse2;
        public Vector3 TerrainLightingAfternoonDiffuse3;
        public Vector3 TerrainLightingAfternoonLightPos;
        public Vector3 TerrainLightingAfternoonLightPos2;
        public Vector3 TerrainLightingAfternoonLightPos3;
        public Vector3 TerrainLightingEveningAmbient;
        public Vector3 TerrainLightingEveningDiffuse;
        public Vector3 TerrainLightingEveningLightPos;
        public Vector3 TerrainLightingMorningAmbient;
        public Vector3 TerrainLightingMorningDiffuse;
        public Vector3 TerrainLightingMorningLightPos;
        public Vector3 TerrainLightingNightAmbient;
        public Vector3 TerrainLightingNightDiffuse;
        public Vector3 TerrainLightingNightLightPos;
        public string TerrainLOD;
        public int TerrainLODTargetTimeMS;
        public Vector3 TerrainObjectsLightingAfternoonAmbient;
        public Vector3 TerrainObjectsLightingAfternoonAmbient2;
        public Vector3 TerrainObjectsLightingAfternoonAmbient3;
        public Vector3 TerrainObjectsLightingAfternoonDiffuse;
        public Vector3 TerrainObjectsLightingAfternoonDiffuse2;
        public Vector3 TerrainObjectsLightingAfternoonDiffuse3;
        public Vector3 TerrainObjectsLightingAfternoonLightPos;
        public Vector3 TerrainObjectsLightingAfternoonLightPos2;
        public Vector3 TerrainObjectsLightingAfternoonLightPos3;
        public Vector3 TerrainObjectsLightingEveningAmbient;
        public Vector3 TerrainObjectsLightingEveningDiffuse;
        public Vector3 TerrainObjectsLightingEveningLightPos;
        public Vector3 TerrainObjectsLightingMorningAmbient;
        public Vector3 TerrainObjectsLightingMorningDiffuse;
        public Vector3 TerrainObjectsLightingMorningLightPos;
        public Vector3 TerrainObjectsLightingNightAmbient;
        public Vector3 TerrainObjectsLightingNightDiffuse;
        public Vector3 TerrainObjectsLightingNightLightPos;
        public float TerrainResourceCellSize;
        public string TimeOfDay;
        public int TintUnitIfPathingForMoreThan;
        public string TreeFadeObjectFilter;

        public float UnitDamagedThreshold;
        public float UnitReallyDamagedThreshold;
        public int UnlookPersistDuration;
        public int Use3WayTerrainBlends;
        public bool UseBehindBuildingMarker;
        public bool UseCameraConstraints;
        public bool UseCameraInReplay;
        public bool UseCloudMap;
        public bool UseCloudPlane;
        public bool UseFPSLimit;
        public bool UseHalfHeightMap;
        public bool UseHelpTextSystem;
        public bool UseLightMap;
        public string UserDataLeafName;
        public bool UseShadowDecals;
        public bool UseShadowMapping;
        public bool UseShadowVolumes;
        public bool UseSimpleHordeDecals;
        public bool UseSimpleMergeDecals;

        public int ValuePerSupplyBox;
        public int VertexWaterAngle1;
        public int VertexWaterAngle2;
        public int VertexWaterAngle3;
        public int VertexWaterAngle4;
        public float VertexWaterAttenuationA1;
        public float VertexWaterAttenuationA2;
        public float VertexWaterAttenuationA3;
        public float VertexWaterAttenuationA4;
        public float VertexWaterAttenuationB1;
        public float VertexWaterAttenuationB2;
        public float VertexWaterAttenuationB3;
        public float VertexWaterAttenuationB4;
        public float VertexWaterAttenuationC1;
        public float VertexWaterAttenuationC2;
        public float VertexWaterAttenuationC3;
        public float VertexWaterAttenuationC4;
        public float VertexWaterAttenuationRange1;
        public float VertexWaterAttenuationRange2;
        public float VertexWaterAttenuationRange3;
        public float VertexWaterAttenuationRange4;
        public string VertexWaterAvailableMaps1;
        public string VertexWaterAvailableMaps2;
        public string VertexWaterAvailableMaps3;
        public string VertexWaterAvailableMaps4;
        public float VertexWaterGridSize1;
        public float VertexWaterGridSize2;
        public float VertexWaterGridSize3;
        public float VertexWaterGridSize4;
        public float VertexWaterHeightClampLow1;
        public float VertexWaterHeightClampLow2;
        public float VertexWaterHeightClampLow3;
        public float VertexWaterHeightClampLow4;
        public float VertexWaterHeightClampHi1;
        public float VertexWaterHeightClampHi2;
        public float VertexWaterHeightClampHi3;
        public float VertexWaterHeightClampHi4;
        public int VertexWaterXGridCells1;
        public int VertexWaterXGridCells2;
        public int VertexWaterXGridCells3;
        public int VertexWaterXGridCells4;
        public float VertexWaterXPosition1;
        public float VertexWaterXPosition2;
        public float VertexWaterXPosition3;
        public float VertexWaterXPosition4;
        public int VertexWaterYGridCells1;
        public int VertexWaterYGridCells2;
        public int VertexWaterYGridCells3;
        public int VertexWaterYGridCells4;
        public float VertexWaterYPosition1;
        public float VertexWaterYPosition2;
        public float VertexWaterYPosition3;
        public float VertexWaterYPosition4;
        public float VertexWaterZPosition1;
        public float VertexWaterZPosition2;
        public float VertexWaterZPosition3;
        public float VertexWaterZPosition4;
        public float VerticalScrollSpeedFactor;
        public string VeterancyPipDrawObjectFilter;
        public string VictoryConditionStructureObjectFilter;
        public string VictoryConditionUnitObjectFilter;
        public bool VideoOn;
        public string VolumeDistribution;
        public float VolumeMuValue;
        public bool VTune;

        public int WaitToForceMemberToEnterDelay;
        public float WaterExtentX;
        public float WaterExtentY;
        public float WaterPositionX;
        public float WaterPositionY;
        public float WaterPositionZ;
        public int WaterType;
        public Dictionary<string, WeaponBonus> weaponBonuses = new Dictionary<string, WeaponBonus>();
        public string Weather;
        public bool Windowed;
        public bool Wireframe;

        public int XResolution;

        public int YResolution;

        



        public void AddStandardPublicBone(string name)
        {
            if (!standardPublicBones.Contains(name))
                standardPublicBones.Add(name);
        }

        public void AddWeaponBonus(string name, string type, int value)
        {
            if (!weaponBonuses.ContainsKey(name))
                weaponBonuses.Add(name, new WeaponBonus().AddBonus(type, value));
            else
                weaponBonuses[name].AddBonus(type, value);
        }
    }

    class WeaponBonus
    {
        public Dictionary<string, int> bonuses = new Dictionary<string, int>();

        public WeaponBonus AddBonus(string _type, int _value)
        {
            if (!bonuses.ContainsKey(_type))
                bonuses.Add(_type, _value);
            else
                bonuses[_type] = _value;
            return this;
        }
    }
}
