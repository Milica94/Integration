using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Common
{
	
	public enum DMSType : short
	{		
		MASK_TYPE							= unchecked((short)0xFFFF),

        BAY                         = 0x0001,
        SERIESCOMPENSATOR           = 0X0002,
        ACLINESEGMENT               = 0X0003,
        DCLINESEGMENT               = 0X0004,
        CNODE                       = 0X0005,
        TERMINAL                    = 0x0006,
       
    }

    [Flags]
	public enum ModelCode : long
	{
		IDOBJ								= 0x1000000000000000,
		IDOBJ_GID							= 0x1000000000000104,
        IDOBJ_ALIAS					        = 0x1000000000000207,
		IDOBJ_MRID							= 0x1000000000000307,
		IDOBJ_NAME							= 0x1000000000000407,

        PSR                                 = 0x1100000000000000,

        CNODE                               = 0X1200000000050000,
        CNODE_DESC                          = 0X1200000000050107,
        CNODE_TERMINALS                     = 0X1200000000050219,
        CNODE_CNC                           = 0X1200000000050309,

        TERMINAL                            = 0X1300000000060000,
        TERMINAL_CE                         = 0X1300000000060109,
        TERMINAL_CN                         = 0X1300000000060209, 
        TERMINAL_CONNECTED                  = 0X1300000000060301,
        TERMINAL_PHASES                     = 0X130000000006040A,
        TERMINAL_SEQUENCENUMBER             = 0X1300000000060503,

        EQUIPMENT                           = 0X1110000000000000,
        EQUIPMENT_EC                        = 0X1110000000000109,
        EQUIPMENT_AGGREGATE                 = 0X1110000000000201,
        EQUIPMENT_NORMALLYSERVICE           = 0X1110000000000301,

        CNCONTAINER                         = 0X1120000000000000,
        CNCONTAINER_CNS                     = 0X1120000000000119,

        CONDEQUIPMENT                       = 0X1111000000000000,
        CONDEQUIPMENT_TERMINALS             = 0X1111000000000119,

        SERIESCOMPENSATOR                   = 0X1111100000020000,
        SERIESCOMPENSATOR_R                 = 0X1111100000020105,
        SERIESCOMPENSATOR_R0                = 0X1111100000020205,
        SERIESCOMPENSATOR_X                 = 0X1111100000020305,
        SERIESCOMPENSATOR_X0                = 0X1111100000020405,


        CONDUCTOR                           = 0X1111200000000000,
        CONDUCTOR_LENGTH                    = 0X1111200000000105,

        EQUIPMENTCONTAINER                  = 0X1121000000000000,
        EQUIPMENTCONTAINER_EQUIPMENTS       = 0X1121000000000119,

        BAY                                 = 0X1121100000010000,
        BAY_ENERGYFLAG                      = 0X1121100000010101,
        BAY_POWERFLAG                       = 0X1121100000010201,

        DCLINESEGMENT                       = 0X1111210000040000,
        DCLINESEGMENT_INDUTANCE             = 0X1111210000040105,
        DCLINESEGMENT_RESISTANCE            = 0X1111210000040205,

        ACLINESEGMENT                       = 0X1111220000030000,
        ACLINESEGMENT_B0CH                  = 0X1111220000030105,
        ACLINESEGMENT_BCH                   = 0X1111220000030205,
        ACLINESEGMENT_G0CH                  = 0X1111220000030305,
        ACLINESEGMENT_GCH                   = 0X1111220000030405,
        ACLINESEGMENT_R                     = 0X1111220000030505,
        ACLINESEGMENT_R0                    = 0X1111220000030605,
        ACLINESEGMENT_X                     = 0X1111220000030705,
        ACLINESEGMENT_X0                    = 0X1111220000030805,
    }

    [Flags]
	public enum ModelCodeMask : long
	{
		MASK_TYPE			 = 0x00000000ffff0000,
		MASK_ATTRIBUTE_INDEX = 0x000000000000ff00,
		MASK_ATTRIBUTE_TYPE	 = 0x00000000000000ff,

		MASK_INHERITANCE_ONLY = unchecked((long)0xffffffff00000000),
		MASK_FIRSTNBL		  = unchecked((long)0xf000000000000000),
		MASK_DELFROMNBL8	  = unchecked((long)0xfffffff000000000),		
	}																		
}
