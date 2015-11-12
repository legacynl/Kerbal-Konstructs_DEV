﻿using System;
using UnityEngine;
using KerbalKonstructs.API;
using KerbalKonstructs.Utilities;

namespace KerbalKonstructs.LaunchSites
{
	public class LaunchSite
	{
		[PersistentKey]
		public string name;

		public string author;
		public SiteType type;
		public Texture logo;
		public Texture icon;
		public string description;

		public string category;
		public float opencost;
		public float closevalue;

		[PersistentField]
		public string openclosestate { get; private set; }

		[PersistentKey]
		public bool isGroundStation;

		[PersistentKey]
		private Guid stationGuid;

		[PersistentField]
		public string favouritesite;

		public float reflon;
		public float reflat;
		public float refalt;
		public float sitelength;
		public float sitewidth;
		public float launchrefund;
		public float recoveryfactor;
		public float recoveryrange;

		public GameObject GameObject;
		public PSystemSetup.SpaceCenterFacility facility;

		public LaunchSite(string sName, string sAuthor, SiteType sType, Texture sLogo, Texture sIcon,
			string sDescription, string sDevice, float fOpenCost, float fCloseValue, string sOpenCloseState, float fRefLon, 
			float fRefLat, float fRefAlt, float fLength, float fWidth, float fRefund, float fRecoveryFactor, float fRecoveryRange,
			GameObject gameObject, PSystemSetup.SpaceCenterFacility newFacility = null, string sFavourite = "No")
		{
			name = sName;
			author = sAuthor;
			type = sType;
			logo = sLogo;
			icon = sIcon;
			description = sDescription;
			category = sDevice;
			opencost = fOpenCost;
			closevalue = fCloseValue;
			openclosestate = sOpenCloseState;
			GameObject = gameObject;
			facility = newFacility;
			reflon = fRefLon;
			reflat = fRefLat;
			refalt = fRefAlt;
			sitelength = fLength;
			sitewidth = fWidth;
			launchrefund = fRefund;
			recoveryfactor = fRecoveryFactor;
			recoveryrange = fRecoveryRange;
			favouritesite = sFavourite;
		}

		public void setOpenClose(string state) {
			openclosestate = state;
		}

		public void OpenLauchsite()
		{
			openclosestate = "Open";
			//if (this.isGroundStation && RTWrapper.RTLoaded)
			if(RTWrapper.RTLoaded)
			{
				Debug.Log ("adding groundstation :)");
				stationGuid = RTWrapper.AddBaseStation (name, 
														(double)reflat,
														(double)reflon,
														(double)refalt,
														1);
				if (stationGuid != default(Guid))
					Debug.Log ("added groundstation guid: "+ stationGuid.ToString ());
			}

		}

		public void CloseLaunchSite()
		{
			openclosestate = "Closed";
			//if (this.isGroundStation && RTWrapper.RTLoaded)
			if(RTWrapper.RTLoaded)
			{
				Debug.Log ("removing groundstation :( met guid: "+ stationGuid.ToString ());
				RTWrapper.RemoveBaseStation (stationGuid);
				stationGuid = default(Guid);
				Debug.Log ("removed groundstation, guid is nu: "+ stationGuid.ToString ());

			}

		}
	}

	public enum SiteType
	{
		VAB,
		SPH,
		Any
	}
}
