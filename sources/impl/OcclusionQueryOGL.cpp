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
#include "impl/OcclusionQueryOGL.h"

#include <GL/GLee.h>


namespace hpl {

	//////////////////////////////////////////////////////////////////////////
	// CONSTRUCTORS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	cOcclusionQueryOGL::cOcclusionQueryOGL()
	{
		glGenQueriesARB(1, (GLuint *)&mlQueryId);
		mlLastSampleCount =0;
	}

	//-----------------------------------------------------------------------

	cOcclusionQueryOGL::~cOcclusionQueryOGL()
	{
		glDeleteQueriesARB(1, (GLuint *)&mlQueryId);
	}

	//-----------------------------------------------------------------------

	//////////////////////////////////////////////////////////////////////////
	// PUBLIC METHODS
	//////////////////////////////////////////////////////////////////////////

	//-----------------------------------------------------------------------

	void cOcclusionQueryOGL::Begin()
	{
		glBeginQueryARB(GL_SAMPLES_PASSED_ARB,mlQueryId);
	}

	void cOcclusionQueryOGL::End()
	{
		glEndQueryARB(GL_SAMPLES_PASSED_ARB);
	}

	bool cOcclusionQueryOGL::FetchResults()
	{
		int lAvailable=0;
		glGetQueryObjectivARB(mlQueryId,GL_QUERY_RESULT_AVAILABLE_ARB,(GLint *)&lAvailable);
		if(lAvailable==0) return false;

		glGetQueryObjectivARB(mlQueryId,GL_QUERY_RESULT_ARB,(GLint *)&mlLastSampleCount);
		return true;
	}

	unsigned int cOcclusionQueryOGL::GetSampleCount()
	{
		return mlLastSampleCount;
	}

	//-----------------------------------------------------------------------
}
