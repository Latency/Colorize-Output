################################################################################
# All rights reserved (R)       Bio-Hazard Industries  (c) - 1999              #
################################################################################
# Description:  Build script for ColorG(CC/++) project.                        #
# Author:       Latency McLaughlin                                             #
################################################################################

# ColorizeOutput version.
VERSION = 2.12

# GNU compiler to use.
CXX     = g++

# Where to install the executable.
BIN_PATH = /usr/bin

# Executable name.
EXE_FILE = colorizeoutput

# Where to install the global configuration file.
RC_PATH = /etc/skel

# Configuration file name.
RC_FILE = "$(EXE_FILE).rc"

# Any special flags you want to pass to the compiler.
CXXFLAGS = -Wall -Wno-trigraphs -O3

# Comment if we want screen output to default 80x24 / default compiler output.
#NOWRAP = 1


##############################################################################
# Do Not Modify Anything Below This Line                                     #
##############################################################################

# Any special flags you want to pass to the compiler.
##DEBUG = 1                     # Uncomment if we need to troubleshoot.

# Current compiler version.  `gcc -v'  or `g++ -v' for versioning info.
COMPILER_VER = $(shell $(BIN_PATH)/$(CXX) -v 2>&1 | grep gcc\ version | cut -d\  -f3)

DEFS    = -DEXE_FILE=\"$(EXE_FILE)\" -DRC_PATH=\"$(RC_PATH)\" \
		  -DCOMPILER_VER=\"$(COMPILER_VER)\" -DVERSION=\"$(VERSION)\"
ifdef DEBUG
  DEFS  += -DDEBUG
endif
ifdef NOWRAP
  DEFS  += -DNOWRAP
endif

# Any libraries that might need to be supported..   e.g. Solaris.
# -lcrypt -lnsl -lsocket -lmalloc -lelf etc..
LIBS =  -lctypes


# General compilation command
.cpp.o:
	@echo "Compiling $<"
	@$(CXX) -o $@ -c $(CXXFLAGS) $(DEFS) $<
	@chmod 600 $@

OBJFILES = $(wildcard ./*.cpp)
OBJFILES := $(OBJFILES:.cpp=.o)
ALLFILES  = $(OBJFILES)


##############################################################################
##############################################################################

$(EXE_FILE): $(ALLFILES)
	@echo "Linking $(PWD)/$@"
	@$(CXX) -o $@  $(ALLFILES) $(LIBS)
	@`which strip` $@

install: $(EXE_FILE)
	@if [ ! -f $(BIN_PATH)/gcc-$(COMPILER_VER) ]; then \
	  echo -e "\e[35mBacking up original compiler \e[34m\`$(BIN_PATH)/gcc-$(COMPILER_VER)'\e[35m.\e[0m" ; \
	  mv $(BIN_PATH)/gcc $(BIN_PATH)/gcc-$(COMPILER_VER) ; \
	 fi
	@if [ ! -f $(BIN_PATH)/g++-$(COMPILER_VER) ]; then \
	  echo -e "\e[35mBacking up original compiler \e[34m\`$(BIN_PATH)/g++-$(COMPILER_VER)'\e[35m.\e[0m" ; \
	  mv $(BIN_PATH)/g++ $(BIN_PATH)/g++-$(COMPILER_VER) ; \
	 fi
	@echo -e "\e[35mInstalling executable \e[1m\`$(BIN_PATH)/$(EXE_FILE)'\e[0;35m.\e[0m"
	@mv $(EXE_FILE) $(BIN_PATH)
	@if [ ! -L $(BIN_PATH)/gcc ]; then \
	  echo -e "\e[35mInstalling link for compiler \e[1;36m\`$(BIN_PATH)/gcc'\e[0;35m.\e[0m" ; \
	  ln -s $(BIN_PATH)/$(EXE_FILE) $(BIN_PATH)/gcc ; \
	 fi
	@if [ ! -L $(BIN_PATH)/g++ ]; then \
	  echo -e "\e[35mInstalling link for compiler \e[1;36m\`$(BIN_PATH)/g++'\e[0;35m.\e[0m" ; \
	  ln -s $(BIN_PATH)/$(EXE_FILE) $(BIN_PATH)/g++ ; \
	 fi
	@if [ ! -f $(RC_PATH)/$(EXE_FILE) ]; then \
	  echo -e "\e[35mInstalling configuration file to \e[1;34m\`$(RC_PATH)/$(EXE_FILE)'\e[0;35m.\e[0m" ; \
	  cp -p $(RC_FILE) $(RC_PATH)/$(EXE_FILE) ; \
	 fi

uninstall:
	@if [ -f $(BIN_PATH)/gcc-$(COMPILER_VER) ]; then \
	  echo -e "\e[35mRemoving link for compiler \e[1;36m\`$(BIN_PATH)/gcc'\e[0;35m.\e[0m" ; \
	  rm $(BIN_PATH)/gcc ; \
	  echo -e "\e[35mRestoring original compiler \e[34m\`$(BIN_PATH)/gcc-$(COMPILER_VER)'\e[35m.\e[0m" ; \
	  mv $(BIN_PATH)/gcc-$(COMPILER_VER) $(BIN_PATH)/gcc ; \
	 fi
	@if [ -f $(BIN_PATH)/g++-$(COMPILER_VER) ]; then \
	  echo -e "\e[35mRemoving link for compiler \e[1;36m\`$(BIN_PATH)/g++'\e[0;35m.\e[0m" ; \
	  rm $(BIN_PATH)/g++ ; \
	  echo -e "\e[35mRestoring original compiler \e[34m\`$(BIN_PATH)/g++-$(COMPILER_VER)'\e[35m.\e[0m" ; \
	  mv $(BIN_PATH)/g++-$(COMPILER_VER) $(BIN_PATH)/g++ ; \
	 fi
	@if [ -f $(BIN_PATH)/$(EXE_FILE) ]; then \
	  echo -e "\e[35mPurging object file \e[1m\`$(BIN_PATH)/$(EXE_FILE)'\e[0;35m.\e[0m" ; \
	  rm $(BIN_PATH)/$(EXE_FILE) ; \
	 fi
	@if [ -f $(RC_PATH)/$(EXE_FILE) ]; then \
	  echo -e "\e[35mUninstalling configuration file \e[1;34m\`$(RC_PATH)/$(EXE_FILE)'\e[0;35m.\e[0m" ; \
	  rm $(RC_PATH)/$(EXE_FILE) ; \
	 fi

force_uninstall:
	cp $(BIN_PATH)/g++-4.0.2 $(BIN_PATH)/gcc
	cp $(BIN_PATH)/g++-4.0.2 $(BIN_PATH)/g++

archive:
	@tar -cpf $(EXE_FILE)-$(VERSION).tar $(ALLFILES:.o=.cpp) $(RC_FILE) Makefile
	@bzip2 $(EXE_FILE)-$(VERSION).tar

# Remove all the compiled objects for a clean compile
clean:
	@echo "Removing object and backup files."
	@$(RM) $(ALLFILES) *~ core

dep depend .depend:
	@echo "Generating source code dependency information."
	@$(CXX) -MM $(CXXFLAGS) $(DEFS) $(ALLFILES:.o=.cpp) > .depend

-include .depend
