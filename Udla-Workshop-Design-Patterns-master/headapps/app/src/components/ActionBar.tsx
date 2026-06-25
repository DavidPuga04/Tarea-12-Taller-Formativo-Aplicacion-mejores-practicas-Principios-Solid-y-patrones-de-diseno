"use client";

import { addExplorer } from "@/actions/add-explorer";

export const ActionBar = () => {
  return (
    <div className="mb-4 flex w-full justify-end">
      <form
        action={addExplorer}
        onSubmit={(e) => {
          e.currentTarget.reset();
          window.location.reload();
        }}
      >
        <button
          type="submit"
          className="rounded bg-blue-500 px-4 py-2 font-semibold text-white hover:bg-blue-600"
        >
          Add Explorer
        </button>
      </form>
    </div>
  );
};
