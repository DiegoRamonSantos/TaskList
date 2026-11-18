/* Main Application Logic Runs after DOM is fully loaded */
document.addEventListener("DOMContentLoaded", () => {

    const themeToggle = document.querySelector("[data-theme-toggle]");
    const preferredThemeQuery = window.matchMedia("(prefers-color-scheme: dark)");

    const getStoredTheme = () => {
        try {
            return localStorage.getItem("taskflow-theme");
        } catch {
            return null;
        }
    };

    const storeTheme = (theme) => {
        try {
            localStorage.setItem("taskflow-theme", theme);
        } catch {
            return;
        }
    };

    const getActiveTheme = () => document.documentElement.dataset.theme === "dark" ? "dark" : "light";

    const setTheme = (theme, shouldPersist) => {
        document.documentElement.dataset.theme = theme;

        if (shouldPersist) {
            storeTheme(theme);
        }

        if (themeToggle) {
            themeToggle.setAttribute(
                "aria-label",
                theme === "dark" ? "Alternar para tema claro" : "Alternar para tema escuro");
            themeToggle.setAttribute("title", theme === "dark" ? "Usar tema claro" : "Usar tema escuro");
        }
    };

    setTheme(getActiveTheme(), false);

    themeToggle?.addEventListener("click", () => {
        setTheme(getActiveTheme() === "dark" ? "light" : "dark", true);
    });

    preferredThemeQuery.addEventListener("change", (event) => {
        if (!getStoredTheme()) {
            setTheme(event.matches ? "dark" : "light", false);
        }
    });

    //  MENU MANAGEMENT
    const menuToggle = document.querySelector("[data-menu-toggle]");
    const menu = document.querySelector("[data-menu]");

    if (menuToggle && menu) {
        const setMenuState = (isOpen) => {
            menuToggle.classList.toggle("is-open", isOpen);
            menu.classList.toggle("is-open", isOpen);
            menuToggle.setAttribute("aria-expanded", isOpen.toString());
            menuToggle.setAttribute("aria-label", isOpen ? "Fechar menu" : "Abrir menu");
        };

        menuToggle.addEventListener("click", () => {
            setMenuState(!menu.classList.contains("is-open"));
        });

        menu.querySelectorAll("a").forEach((link) => {
            link.addEventListener("click", () => setMenuState(false));
        });

        document.addEventListener("click", (event) => {
            if (!menu.classList.contains("is-open")) {
                return;
            }

            const target = event.target;
            if (target instanceof Node && !menu.contains(target) && !menuToggle.contains(target)) {
                setMenuState(false);
            }
        });

        document.addEventListener("keydown", (event) => {
            if (event.key === "Escape") {
                setMenuState(false);
            }
        });

        window.addEventListener("resize", () => {
            if (window.matchMedia("(min-width: 760px)").matches) {
                setMenuState(false);
            }
        });
    }

    //  TASK CARD INTERACTIONS
    document.querySelectorAll("[data-task-card] .toggle-form").forEach((form) => {
        form.addEventListener("submit", () => {
            // Add visual feedback while form is being submitted
            form.closest("[data-task-card]")?.classList.add("is-toggling");
        });
    });

    //  TASK FILTER TABS
    const taskFilterTabs = Array.from(document.querySelectorAll("[data-task-filter-tab]"));

    if (taskFilterTabs.length > 0) {
        const taskFilterPanels = new Map(
            Array.from(document.querySelectorAll("[data-task-filter-panel]"))
                .map((panel) => [panel.dataset.taskFilterPanel, panel])
        );

        const setActiveTaskTab = (activeTab) => {
            const activeKey = activeTab.dataset.taskFilterTab;
            taskFilterTabs.forEach((tab) => {
                const isActive = tab === activeTab;
                tab.classList.toggle("is-active", isActive);
                tab.setAttribute("aria-selected", isActive.toString());
                tab.setAttribute("tabindex", isActive ? "0" : "-1");
            });
            taskFilterPanels.forEach((panel, key) => {
                const isActive = key === activeKey;
                panel.hidden = !isActive;
                panel.classList.toggle("is-active", isActive);
            });
        };

        taskFilterTabs.forEach((tab, index) => {
            tab.addEventListener("click", () => setActiveTaskTab(tab));

            tab.addEventListener("keydown", (event) => {
                const lastIndex = taskFilterTabs.length - 1;
                let nextIndex = index;

                if (event.key === "ArrowRight") {
                    nextIndex = index === lastIndex ? 0 : index + 1;
                } else if (event.key === "ArrowLeft") {
                    nextIndex = index === 0 ? lastIndex : index - 1;
                } else if (event.key === "Home") {
                    nextIndex = 0;
                } else if (event.key === "End") {
                    nextIndex = lastIndex;
                } else {
                    return;
                }

                event.preventDefault();
                const nextTab = taskFilterTabs[nextIndex];
                nextTab.focus();
                setActiveTaskTab(nextTab);
            });
        });
    }

    //  TOAST NOTIFICATIONS
    const toast = document.querySelector("[data-toast]");

    if (toast) {
        window.setTimeout(() => {
            toast.classList.add("is-hiding");
            toast.addEventListener("animationend", () => toast.remove(), { once: true });
        }, 3200);
    }
});
