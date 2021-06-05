local M = {}

function M.Random(a,b)
    math.randomseed(tostring(ngx.now()):reverse():sub(1, 6))
    return math.random(a,b)
end

return M