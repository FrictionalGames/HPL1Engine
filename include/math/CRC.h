/*
 * Copyright (C) 2006-2010 - Frictional Games
 *
 * This file is part of HPL1 Engine.
 *
 * HPL1 Engine is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * HPL1 Engine is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with HPL1 Engine.  If not, see <http://www.gnu.org/licenses/>.
 */
#ifndef HPL_CRC_H
#define HPL_CRC_H

#include <stdint.h>

namespace hpl {
	//----------------------------------------

	typedef uint32_t tCRCKey;

	//----------------------------------------

	class cCRCTable
	{
	public:
		cCRCTable () : mKey (0) {}

		void Init (tCRCKey key);

		tCRCKey operator [] (unsigned i){return mTable [i];}

	private:
		tCRCKey mTable [256];
		tCRCKey mKey;
	};

	//----------------------------------------

	class cCRC
	{
	public:
		cCRC (tCRCKey key) : mKey (key), mRegister(0)
		{
			mTable.Init (key);
		}

		void PutByte (unsigned aByte);

		tCRCKey Done ()
		{
			tCRCKey temp = mRegister;
			mRegister = 0;
			return temp;
		}

	private:
		static cCRCTable mTable;
		tCRCKey mKey;
		tCRCKey mRegister;
	};

	//----------------------------------------
}

#endif // HPL_CRC_H
