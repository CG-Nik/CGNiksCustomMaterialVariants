using Alta.Caves;
using MateriaLib;
using MelonLoader;
using UnityEngine;
using DyeingAPI;
using CustomDistributionAPI;
using MaterialDistributionAPI;
using System.Reflection;

[assembly: MelonInfo(typeof(CGNiksCustomMaterialVariants.Core), "CGNiksCustomMaterialVariants", "1.0.0", "CGNik", null)]
[assembly: MelonGame("Alta", "A Township Tale")]

namespace CGNiksCustomMaterialVariants
{
    public class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
            MateriaLib.Main.SetupMaterial += SetupMaterial;
            DyeingAPI.Core.SetUpDyeRecipes += SetUpDyeRecipes;
        }

        private static LibMaterial AddLeather(string name, int hash, MaterialConfig materialConfig, Vector4 color0, Vector4 color1, Vector4 color2, bool addToDistribution, float baseValue = 1f, float noAttributeValue = 1f, AttributeCurveRange[] multipliers = null)
        {
            LibMaterial libMaterial = new LibMaterial(name, hash, LibMaterial.MaterialType.leather);

            LibMaterial.NewMaterials.Add(libMaterial);

            libMaterial.Configure(materialConfig);

            Material material = UnityEngine.Object.Instantiate(libMaterial.physicalMaterial.GetMaterial(PhysicalMaterialChannel.A));

            material.name = name;

            material.SetVector("_Color", color0);
            material.SetVector("_Color1", color1);
            material.SetVector("_Color2", color2);

            libMaterial.ReplaceAllMaterials(material);

            if (addToDistribution)
            {
                Distribution leatherMaterialDistribution = Distribution.All.Where(dist => dist.Hash == 49220u).First();
                CustomDistributionAPI.Core.AddToDistribution(leatherMaterialDistribution, libMaterial.physicalMaterial, baseValue, noAttributeValue, multipliers);
            }

            return libMaterial;
        }

        private static LibMaterial AddCanvas(string name, int hash, MaterialConfig materialConfig, Vector4 colorA_worn, Vector4 color_worn, Vector4 colorA_cutout, Vector4 color_cutout, bool addToDistribution, float baseValue = 1f, float noAttributeValue = 1f, AttributeCurveRange[] multipliers = null)
        {
            LibMaterial libMaterial = new LibMaterial(name, hash, LibMaterial.MaterialType.canvas);

            LibMaterial.NewMaterials.Add(libMaterial);

            libMaterial.Configure(materialConfig);

            Material worn = UnityEngine.Object.Instantiate(libMaterial.physicalMaterial.GetMaterial(PhysicalMaterialChannel.A));
            Material cutout = UnityEngine.Object.Instantiate(libMaterial.physicalMaterial.GetMaterial(PhysicalMaterialChannel.B));

            worn.name = name + " Worn";
            cutout.name = name + " Cutout";

            worn.SetVector("_ColorA", colorA_worn);
            worn.SetVector("_Color", color_worn);
            cutout.SetVector("_ColorA", colorA_cutout);
            cutout.SetVector("_Color", color_cutout);

            libMaterial.ReplaceAllMaterials(worn, cutout);

            if (addToDistribution)
            {
                CustomDistributionAPI.Core.AddToDistribution(MaterialDistributionAPI.Core.canvasMaterialDistribution, libMaterial.physicalMaterial, baseValue, noAttributeValue, multipliers);
            }

            return libMaterial;
        }

        private static LibMaterial AddRope(string name, int hash, MaterialConfig materialConfig, Vector4 colorA, Vector4 color, bool addToDistribution, float baseValue = 1f, float noAttributeValue = 1f, AttributeCurveRange[] multipliers = null)
        {
            LibMaterial libMaterial = new LibMaterial(name, hash, LibMaterial.MaterialType.rope);

            LibMaterial.NewMaterials.Add(libMaterial);

            libMaterial.Configure(materialConfig);

            Material mateial = UnityEngine.Object.Instantiate(libMaterial.physicalMaterial.GetMaterial(PhysicalMaterialChannel.A));

            mateial.name = name;

            mateial.SetVector("_ColorA", colorA);
            mateial.SetVector("_Color", color);

            libMaterial.ReplaceAllMaterials(mateial);

            if (addToDistribution)
            {
                CustomDistributionAPI.Core.AddToDistribution(MaterialDistributionAPI.Core.ropeMaterialDistribution, libMaterial.physicalMaterial, baseValue, noAttributeValue, multipliers);
            }

            return libMaterial;
        }

        public static void SetupMaterial()
        {
            AddLeather(
                "Natural Leather",
                37301,
                new MaterialConfig() { NailHealthMultiplier = 0.6f, MaxCraftingDamageMultiplier = 1f },
                new Vector4(0.35f * 1.5f, 0.14f * 1.5f, 0.06f * 1.5f, 1f),
                new Vector4(0.7f * 1.3f, 0.45f * 1.3f, 0.2f * 1.3f, 1f),
                new Vector4(0.91f * 1.1f, 0.59f * 1.1f, 0.35f * 1.1f, 1f),
                true,
                1f,
                1f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "Wyrmness").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0f),
                            new Keyframe(1f, 1f)
                        }),
                        1f,
                        0f
                    ),
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 1f),
                            new Keyframe(1f, 0f)
                        }),
                        0f,
                        30f
                    )
                }
            );

            AddLeather(
                "Charcoal Leather",
                37302,
                new MaterialConfig() { WeightMultiplier = 0.9f, NailHealthMultiplier = 1.1f, MaxCraftingDamageMultiplier = 0.9f },
                new Vector4(0.05f, 0.05f, 0.05f, 1f),
                new Vector4(0.15f, 0.15f, 0.15f, 1f),
                new Vector4(0.25f, 0.25f, 0.25f, 1f),
                true,
                0.35f,
                0.35f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "Wyrmness").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0f),
                            new Keyframe(1f, 1f)
                        }),
                        1f,
                        0f
                    ),
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0.25f),
                            new Keyframe(1f, 1f)
                        }),
                        0f,
                        100f
                    )
                }
            );

            AddLeather(
                "Mahogany Leather",
                37303,
                new MaterialConfig() { WeightMultiplier = 0.95f },
                new Vector4(0.15f, 0.05f, 0f, 1f),
                new Vector4(0.3f, 0.1f, 0f, 1f),
                new Vector4(0.45f, 0.15f, 0f, 1f),
                true,
                0.35f,
                0.35f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "Wyrmness").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0f),
                            new Keyframe(1f, 1f)
                        }),
                        1f,
                        0f
                    ),
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0.25f),
                            new Keyframe(0.5f, 1f),
                            new Keyframe(1f, 0.25f)
                        }),
                        10f,
                        40f
                    )
                }
            );

            AddLeather(
                "Walnut Leather",
                37304,
                new MaterialConfig() { WeightMultiplier = 0.8f, NailHealthMultiplier = 1.5f, MaxCraftingDamageMultiplier = 0.7f },
                new Vector4(0.1f, 0.05f, 0.025f, 1f),
                new Vector4(0.2f, 0.1f, 0.05f, 1f),
                new Vector4(0.4f, 0.2f, 0.1f, 1f),
                true,
                0.35f,
                0.35f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "Wyrmness").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0f),
                            new Keyframe(1f, 1f)
                        }),
                        1f,
                        0f
                    ),
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0f),
                            new Keyframe(0.5f, 1f),
                            new Keyframe(1f, 0.25f)
                        }),
                        40f,
                        60f
                    )
                }
            );

            LibMaterial wyrmStomachLeather = AddLeather(
                "Wyrm Stomach Leather",
                37305,
                new MaterialConfig() { WeightMultiplier = 1.2f, NailHealthMultiplier = 1.4f, MaxCraftingDamageMultiplier = 0.8f },
                new Vector4(0.85f * 0.7f * 0.85f, 0.65f * 0.7f * 0.85f, 0.5f * 0.7f * 0.85f, 1f),
                new Vector4(0.85f * 0.7f, 0.65f * 0.7f, 0.5f * 0.7f, 1f),
                new Vector4(0.85f * 0.7f * 1.15f, 0.65f * 0.7f * 1.15f, 0.5f * 0.7f * 1.15f, 1f),
                true,
                0.5f,
                0f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "Wyrmness").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0f),
                            new Keyframe(1f, 1f)
                        }),
                        0f,
                        1f
                    )
                }
            );

            CustomDistributionAPI.Core.AddToDistribution(
                MaterialDistributionAPI.Core.wyrmMaterialDistribution,
                wyrmStomachLeather.physicalMaterial,
                0.5f,
                0.5f,
                new AttributeCurveRange[] { }
            );

            LibMaterial wyrmBackLeather = AddLeather(
                "Wyrm Back Leather",
                37306,
                new MaterialConfig() { WeightMultiplier = 0.8f, NailHealthMultiplier = 1.6f, MaxCraftingDamageMultiplier = 0.6f },
                new Vector4(0.3f, 0.15f, 0.12f, 1f),
                new Vector4(0.5f, 0.25f, 0.2f, 1f),
                new Vector4(0.7f, 0.35f, 0.28f, 1f),
                true,
                0.5f,
                0f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "Wyrmness").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0f),
                            new Keyframe(1f, 1f)
                        }),
                        0f,
                        1f
                    )
                }
            );

            CustomDistributionAPI.Core.AddToDistribution(
                MaterialDistributionAPI.Core.wyrmMaterialDistribution,
                wyrmBackLeather.physicalMaterial,
                0.5f,
                0.5f,
                new AttributeCurveRange[] { }
            );

            LibMaterial crystalWyrmFaceLeather = AddLeather(
                "Crystal Wyrm Face Leather",
                37307,
                new MaterialConfig() { WeightMultiplier = 0.6f, NailHealthMultiplier = 2f, MaxCraftingDamageMultiplier = 0.5f },
                new Vector4(0.19f, 0.18f, 0.2f, 1f),
                new Vector4(0.325f, 0.35f, 0.3f, 1f),
                new Vector4(0.41f, 0.42f, 0.4f, 1f),
                true,
                1f,
                0f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "Wyrmness").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0f),
                            new Keyframe(1f, 1f)
                        }),
                        0f,
                        1f
                    )
                }
            );

            CustomDistributionAPI.Core.AddToDistribution(
                MaterialDistributionAPI.Core.crystalWyrmMaterialDistribution,
                crystalWyrmFaceLeather.physicalMaterial,
                1f,
                1f,
                new AttributeCurveRange[] { }
            );

            LibMaterial crystalWyrmStomachLeather = AddLeather(
                "Crystal Wyrm Stomach Leather",
                37308,
                new MaterialConfig() { WeightMultiplier = 1.3f, NailHealthMultiplier = 0.8f, MaxCraftingDamageMultiplier = 0.5f },
                new Vector4(0.5f * 0.7f, 0.5f * 0.7f, 0.45f * 0.7f, 1f),
                new Vector4(0.7f * 0.7f, 0.75f * 0.7f, 0.6f * 0.7f, 1f),
                new Vector4(0.9f * 0.7f, 1f * 0.7f, 0.75f * 0.7f, 1f),
                true,
                0.5f,
                0f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "Wyrmness").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0f),
                            new Keyframe(1f, 1f)
                        }),
                        0f,
                        1f
                    )
                }
            );

            CustomDistributionAPI.Core.AddToDistribution(
                MaterialDistributionAPI.Core.crystalWyrmMaterialDistribution,
                crystalWyrmStomachLeather.physicalMaterial,
                0.5f,
                0.5f,
                new AttributeCurveRange[] { }
            );

            LibMaterial crystalWyrmBackLeather = AddLeather(
                "Crystal Wyrm Back Leather",
                37309,
                new MaterialConfig() { WeightMultiplier = 1.1f, NailHealthMultiplier = 1.3f, MaxCraftingDamageMultiplier = 0.9f },
                new Vector4(0.09f, 0.11f, 0.08f, 1f),
                new Vector4(0.19f, 0.22f, 0.16f, 1f),
                new Vector4(0.29f, 0.33f, 0.24f, 1f),
                true,
                0.5f,
                0f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "Wyrmness").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0f),
                            new Keyframe(1f, 1f)
                        }),
                        0f,
                        1f
                    )
                }
            );

            CustomDistributionAPI.Core.AddToDistribution(
                MaterialDistributionAPI.Core.crystalWyrmMaterialDistribution,
                crystalWyrmBackLeather.physicalMaterial,
                0.5f,
                0.5f,
                new AttributeCurveRange[] { }
            );

            AddLeather(
                "White Leather",
                37310,
                new MaterialConfig() { NailHealthMultiplier = 0.8f, MaxCraftingDamageMultiplier = 0.8f },
                new Vector4(0.6f, 0.6f, 0.6f, 1f),
                new Vector4(0.8f, 0.8f, 0.8f, 1f),
                new Vector4(1f, 1f, 1f, 1f),
                true,
                0.35f,
                0.35f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "Wyrmness").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0f),
                            new Keyframe(1f, 1f)
                        }),
                        1f,
                        0f
                    ),
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 1f),
                            new Keyframe(1f, 0f)
                        }),
                        0f,
                        50f
                    )
                }
            );

            AddLeather(
                "Pink Leather",
                37311,
                new MaterialConfig() { NailHealthMultiplier = 0.9f, MaxCraftingDamageMultiplier = 0.9f },
                Color.HSVToRGB(312f / 360f, 0.915f, 0.87f * 2 / 3f),
                Color.HSVToRGB(312f / 360f, 0.915f, 0.75f),
                Color.HSVToRGB(312f / 360f, 0.915f, 1f),
                false
            );

            AddLeather(
                "Dark Blue Leather",
                37312,
                new MaterialConfig() { NailHealthMultiplier = 1.2f, MaxCraftingDamageMultiplier = 0.8f },
                new Vector4(0.14f * 0.67f, 0.25f * 0.67f, 0.375f * 0.67f, 1f),
                new Vector4(0.14f, 0.25f, 0.375f, 1f),
                new Vector4(0.14f * 1.33f, 0.25f * 1.33f, 0.375f * 1.33f, 1f),
                false
            );

            AddCanvas(
                "Light Gray Canvas",
                61791,
                new MaterialConfig() { WeightMultiplier = 0.95f, NailHealthMultiplier = 0.9f, MaxCraftingDamageMultiplier = 1.1f },
                new Vector4(0.53f, 0.53f, 0.53f, 1f),
                new Vector4(0.6f, 0.6f, 0.6f, 1f),
                new Vector4(0.565f, 0.565f, 0.565f, 1f),
                new Vector4(0.6f, 0.6f, 0.6f, 1f),
                true,
                0.5f,
                0.5f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0f),
                            new Keyframe(1f, 1f)
                        }),
                        0f,
                        20f
                    )
                }
            );

            AddCanvas(
                "Dark Gray Canvas",
                61792,
                new MaterialConfig() { WeightMultiplier = 0.8f, NailHealthMultiplier = 1.2f, MaxCraftingDamageMultiplier = 0.8f },
                new Vector4(0.26f, 0.26f, 0.26f, 1f),
                new Vector4(0.3f, 0.3f, 0.3f, 1f),
                new Vector4(0.28f, 0.28f, 0.28f, 1f),
                new Vector4(0.3f, 0.3f, 0.3f, 1f),
                true,
                0.5f,
                0.5f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0f),
                            new Keyframe(1f, 1f)
                        }),
                        20f,
                        40f
                    )
                }
            );

            AddCanvas(
                "White Canvas",
                61793,
                new MaterialConfig() { },
                new Vector4(0.7f, 0.7f, 0.65f, 1f),
                new Vector4(0.8f, 0.8f, 0.75f, 1f),
                new Vector4(0.75f, 0.75f, 0.7f, 1f),
                new Vector4(0.8f, 0.8f, 0.75f, 1f),
                true,
                0.5f,
                0.5f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 1f),
                            new Keyframe(1f, 0f)
                        }),
                        0f,
                        40f
                    )
                }
            );

            AddCanvas(
                "Light Brown Canvas",
                61794,
                new MaterialConfig() { WeightMultiplier = 1.1f, NailHealthMultiplier = 0.6f, MaxCraftingDamageMultiplier = 0.6f },
                new Vector4(0.75f, 0.51f, 0.19f, 1f),
                new Vector4(0.85f, 0.55f, 0.2f, 1f),
                new Vector4(0.8f, 0.53f, 0.195f, 1f),
                new Vector4(0.85f, 0.55f, 0.2f, 1f),
                true,
                0.5f,
                0.5f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0.5f),
                            new Keyframe(1f, 0.3f)
                        }),
                        0f,
                        100f
                    )
                }
            );

            AddCanvas(
                "Dark Brown Canvas",
                61795,
                new MaterialConfig() { WeightMultiplier = 1.2f, NailHealthMultiplier = 0.8f, MaxCraftingDamageMultiplier = 0.8f },
                new Vector4(0.75f * 0.35f, 0.51f * 0.35f, 0.19f * 0.35f, 1f),
                new Vector4(0.85f * 0.35f, 0.55f * 0.35f, 0.2f * 0.35f, 1f),
                new Vector4(0.8f * 0.35f, 0.53f * 0.35f, 0.195f * 0.35f, 1f),
                new Vector4(0.85f * 0.35f, 0.55f * 0.35f, 0.2f * 0.35f, 1f),
                true,
                0.5f,
                0.5f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0.1f),
                            new Keyframe(1f, 0.7f)
                        }),
                        0f,
                        100f
                    )
                }
            );

            AddCanvas(
                "Black Canvas",
                61796,
                new MaterialConfig() { NailHealthMultiplier = 1.2f },
                new Vector4(0.12f, 0.12f, 0.12f, 1f),
                new Vector4(0.15f, 0.15f, 0.15f, 1f),
                new Vector4(0.135f, 0.135f, 0.135f, 1f),
                new Vector4(0.15f, 0.15f, 0.15f, 1f),
                true,
                0.5f,
                0.5f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0f),
                            new Keyframe(1f, 1f)
                        }),
                        0f,
                        100f
                    )
                }
            );

            AddCanvas(
                "Pink Canvas",
                61797,
                new MaterialConfig() { NailHealthMultiplier = 0.9f, MaxCraftingDamageMultiplier = 0.9f },
                Color.HSVToRGB(312f / 360f, 0.915f, 0.87f * 7 / 8f),
                Color.HSVToRGB(312f / 360f, 0.915f, 0.87f),
                Color.HSVToRGB(312f / 360f, 0.915f, 0.87f * 15 / 16f),
                Color.HSVToRGB(312f / 360f, 0.915f, 0.87f),
                false
            );

            AddCanvas(
                "Dark Blue Canvas",
                61798,
                new MaterialConfig() { NailHealthMultiplier = 1.2f, MaxCraftingDamageMultiplier = 0.8f },
                new Vector4(0.14f * 0.8f, 0.25f * 0.8f, 0.375f * 0.8f, 1f),
                new Vector4(0.14f, 0.25f, 0.375f, 1f),
                new Vector4(0.14f * 0.9f, 0.25f * 0.9f, 0.375f * 0.9f, 1f),
                new Vector4(0.14f, 0.25f, 0.375f, 1f),
                false
            );

            AddRope(
                "White Rope",
                35211,
                new MaterialConfig() { },
                new Vector4(0.7f, 0.7f, 0.7f, 1f),
                new Vector4(0.9f, 0.9f, 0.9f, 1f),
                true,
                0.35f,
                0.35f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 1f),
                            new Keyframe(1f, 0f)
                        }),
                        0f,
                        50f
                    )
                }
            );

            AddRope(
                "Light Gray Rope",
                35212,
                new MaterialConfig() { },
                new Vector4(0.4f, 0.4f, 0.4f, 1f),
                new Vector4(0.6f, 0.6f, 0.6f, 1f),
                true,
                0.4f,
                0.4f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0f),
                            new Keyframe(1f, 1f)
                        }),
                        0f,
                        10f
                    )
                }
            );

            AddRope(
                "Dark Gray Rope",
                35213,
                new MaterialConfig() { },
                new Vector4(0.25f, 0.25f, 0.25f, 1f),
                new Vector4(0.35f, 0.35f, 0.35f, 1f),
                true,
                0.4f,
                0.4f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0.5f),
                            new Keyframe(1f, 0f)
                        }),
                        0f,
                        100f
                    )
                }
            );

            AddRope(
                "Black Rope",
                35214,
                new MaterialConfig() { },
                new Vector4(0.1f, 0.1f, 0.1f, 1f),
                new Vector4(0.2f, 0.2f, 0.2f, 1f),
                true,
                0.35f,
                0.35f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0.25f),
                            new Keyframe(1f, 1f)
                        }),
                        40f,
                        60f
                    )
                }
            );

            AddRope(
                "Light Brown Rope",
                35215,
                new MaterialConfig() { },
                new Vector4(0.47f * 1.4f, 0.3f * 1.4f, 0.11f * 1.4f, 1f),
                new Vector4(0.6f * 1.4f, 0.4f * 1.4f, 0.18f * 1.4f, 1f),
                true,
                0.5f,
                0.5f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0.5f),
                            new Keyframe(1f, 1f)
                        }),
                        0f,
                        30f
                    )
                }
            );

            AddRope(
                "Dark Brown Rope",
                35216,
                new MaterialConfig() { },
                new Vector4(0.47f * 0.7f, 0.3f * 0.7f, 0.11f * 0.7f, 1f),
                new Vector4(0.6f * 0.7f, 0.4f * 0.7f, 0.18f * 0.7f, 1f),
                true,
                0.5f,
                0.5f,
                new AttributeCurveRange[]
                {
                    new AttributeCurveRange(
                        (BiomeAttribute)Resources.FindObjectsOfTypeAll(typeof(BiomeAttribute)).Where(attribute => attribute.name == "_Depth").First(),
                        new AnimationCurve(new Keyframe[]
                        {
                            new Keyframe(0f, 0.5f),
                            new Keyframe(1f, 1f)
                        }),
                        30f,
                        60f
                    )
                }
            );

            AddRope(
                "Pink Rope",
                35217,
                new MaterialConfig() { NailHealthMultiplier = 0.9f, MaxCraftingDamageMultiplier = 0.9f },
                Color.HSVToRGB(312f / 360f, 0.915f, 0.87f * 0.75f),
                Color.HSVToRGB(312f / 360f, 0.915f, 0.87f),
                false
            );

            AddRope(
                "Dark Blue Rope",
                35218,
                new MaterialConfig() { NailHealthMultiplier = 1.2f, MaxCraftingDamageMultiplier = 0.8f },
                new Vector4(0.14f * 0.75f, 0.25f * 0.75f, 0.375f * 0.75f, 1f),
                new Vector4(0.14f, 0.25f, 0.375f, 1f),
                false
            );

            List<Distribution.Item> items = (List<Distribution.Item>)MaterialDistributionAPI.Core.crystalWyrmMaterialDistribution.GetType().GetField("items", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(MaterialDistributionAPI.Core.crystalWyrmMaterialDistribution);

            for (int i = items.Count - 1; i >= 0; i--)
            {

                if (items[i].Topic == PhysicalMaterial.All.Where(mat => mat.Hash == 63538u).First())
                {

                    items.RemoveAt(i);

                }

            }
        }

        private void SetUpDyeRecipes()
        {
            DyeRecipe.Create(
                "Pink Leather Recipe",
                37201,
                DyeingAPI.Core.leatherPrefabHashes,
                [10201u],
                false,
                [27100u],
                37311u,
                [Color.HSVToRGB(312f/360f, 0.915f, 0.87f)],
                15,
                0.15f,
                0.15f,
                [44872u]
            );

            DyeRecipe.Create(
                "Pink Canvas Recipe",
                37202,
                DyeingAPI.Core.canvasPrefabHashes,
                [10202u],
                false,
                [27100u],
                61797u,
                [Color.HSVToRGB(312f / 360f, 0.915f, 0.87f)],
                15,
                0.15f,
                0.15f,
                [44872u]
            );

            DyeRecipe.Create(
                "Pink Rope Recipe",
                37203,
                DyeingAPI.Core.ropePrefabHashes,
                [10203u],
                false,
                [27100u],
                35217u,
                [Color.HSVToRGB(312f / 360f, 0.915f, 0.87f)],
                15,
                0.15f,
                0.15f,
                [44872u]
            );

            DyeRecipe.Create(
                "Charcoal Leather Recipe",
                37204,
                DyeingAPI.Core.leatherPrefabHashes,
                [10201u],
                false,
                [],
                37302u,
                [Color.HSVToRGB(180 / 360f, 0.5f, 0.1f)],
                180,
                0.5f,
                0.1f,
                [44872u]
            );

            DyeRecipe.Create(
                "Black Canvas Recipe",
                37205,
                DyeingAPI.Core.canvasPrefabHashes,
                [10202u],
                false,
                [],
                61796u,
                [Color.HSVToRGB(180 / 360f, 0.5f, 0.1f)],
                180,
                0.5f,
                0.1f,
                [44872u]
            );

            DyeRecipe.Create(
                "Black Rope Recipe",
                37206,
                DyeingAPI.Core.ropePrefabHashes,
                [10203u],
                false,
                [],
                35214u,
                [Color.HSVToRGB(180 / 360f, 0.5f, 0.1f)],
                180,
                0.5f,
                0.1f,
                [44872u]
            );

            DyeRecipe.Create(
                "Dark Blue Leather Recipe",
                37207,
                DyeingAPI.Core.leatherPrefabHashes,
                [10201u],
                false,
                [1180u],
                37312u,
                [new Color(0.14f, 0.25f, 0.375f)],
                15,
                0.15f,
                0.15f,
                [44872u]
            );

            DyeRecipe.Create(
                "Dark Blue Canvas Recipe",
                37208,
                DyeingAPI.Core.canvasPrefabHashes,
                [10202u],
                false,
                [1180u],
                61798u,
                [new Color(0.14f, 0.25f, 0.375f)],
                15,
                0.15f,
                0.15f,
                [44872u]
            );

            DyeRecipe.Create(
                "Dark Blue Rope Recipe",
                37209,
                DyeingAPI.Core.ropePrefabHashes,
                [10203u],
                false,
                [1180u],
                35218u,
                [new Color(0.14f, 0.25f, 0.375f)],
                15,
                0.15f,
                0.15f,
                [44872u]
            );

            DyeRecipe.Create(
                "White Leather Recipe",
                37210,
                DyeingAPI.Core.leatherPrefabHashes,
                [10201u],
                false,
                [],
                37310u,
                [new Color(0.8f, 0.8f, 0.8f)],
                15,
                0.15f,
                0.15f,
                [44872u]
            );

            DyeRecipe.Create(
                "White Canvas Recipe",
                37211,
                DyeingAPI.Core.canvasPrefabHashes,
                [10202u],
                false,
                [],
                61793u,
                [new Color(0.8f, 0.8f, 0.8f)],
                15,
                0.15f,
                0.15f,
                [44872u]
            );

            DyeRecipe.Create(
                "White Rope Recipe",
                37212,
                DyeingAPI.Core.ropePrefabHashes,
                [10203u],
                false,
                [],
                35211u,
                [new Color(0.8f, 0.8f, 0.8f)],
                15,
                0.15f,
                0.15f,
                [44872u]
            );
        }
    }
}